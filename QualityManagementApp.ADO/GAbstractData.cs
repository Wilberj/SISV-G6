using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QualityManagementApp.ADO
{
    public abstract class GAbstractData
    {
        protected IDbConnection QMAConnection = null!;
        protected IDbTransaction? QMATransaction;
        protected bool InTransaction;

        protected abstract IDbConnection CreateConnection(string connection);
        protected abstract IDbCommand SQLCommand(string SQLcommand, IDbConnection connection);
        protected abstract IDataAdapter CreateDataAdapterSQL(string SQLCommand, IDbConnection connection);
        protected abstract IDataAdapter CreateDataAdapterSQL(IDbCommand SQLCommand);
        protected abstract List<EntityProps> DescribeEntity(string entityName);
        protected abstract private DataTable BuildTable(object obj, ref string condSQL);
        protected abstract string BuildInsertQueryByObject(object obj);
        protected abstract string BuildUpdateQueryByObject(object obj, string WhereProp);
        protected abstract string BuildUpdateQueryByObject(object obj, string[] WhereProps);
        protected abstract string BuildDeleteQuery(object obj);
        
        public object ExecuteSqlQuery(string strQuery)
        {
            if(QMAConnection.State == ConnectionState.Closed) QMAConnection.Open();
            var command = SQLCommand(strQuery, QMAConnection);
            var Scalar = command.ExecuteScalar();
            QMAConnection.Close();
            if (Scalar == (object)DBNull.Value) return true;
            else return Convert.ToInt32(Scalar);
        }

        public Object InsertObject(Object obj)
        {
            try
            {
                if (QMAConnection.State == ConnectionState.Open) QMAConnection.Close();
                QMAConnection.Open();
                string strQuery = BuildInsertQueryByObject(obj);
                return ExecuteSqlQuery(strQuery);
            }
            catch (Exception)
            {
                QMAConnection.Close();
                throw;
            }
        }

        public Object UpdateObject(Object obj, string[] WhereParams)
        {
            try
            {
                string strQuery = BuildUpdateQueryByObject(obj, WhereParams);
                return ExecuteSqlQuery(strQuery);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Object UpdateObject(Object obj, string WhereParam)
        {
            try
            {
                string strQuery = BuildUpdateQueryByObject(obj, WhereParam);
                return ExecuteSqlQuery(strQuery);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Object Delete(Object obj)
        {
            string strQuery = BuildDeleteQuery(obj);
            return ExecuteSqlQuery(strQuery);
        }

        public DataTable GetDataSQL(string queryString)
        {
            if(QMAConnection.State == ConnectionState.Open) QMAConnection.Close();
            QMAConnection.Open();
            DataSet ObjDS = new();
            CreateDataAdapterSQL(queryString, QMAConnection).Fill(ObjDS);
            return ObjDS.Tables[0].Copy();
        }

        //
        //public DataTable GetDataSQL(IDbCommand Command)
        //{
        //    DataSet ObjDS = new DataSet();
        //    CreateDataAdapterSQL(Command).Fill(ObjDS);
        //    return ObjDS.Tables[0].Copy();
        //}
        public List<T> TakeList<T>(Object obj, string CondSQL = "")
        {
            try
            {
                DataTable Table = BuildTable(obj, ref CondSQL);
                List<T> ListD = ConvertDataTable<T>(Table, obj);
                return ListD;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public T TakeObject<T>(Object obj, string CondSQL = "")
        {
            try
            {
                DataTable Table = BuildTable(obj, ref CondSQL);
                List<T> ListD = ConvertDataTable<T>(Table, obj);
                return ListD[0];
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected static List<T> ConvertDataTable<T>(DataTable datatable, Object obj)
        {
            List<T> data = new();
            foreach (DataRow dr in datatable.Rows)
            {
                T Obj = ConvertRows<T>(obj, dr);
                data.Add(Obj);
            }
            return data;
        }

        private static T ConvertRows<T>(Object obj, DataRow dr)
        {
            var Act = Activator.CreateInstance<T>();
            Type temp = obj.GetType();
            foreach (DataColumn column in dr.Table.Columns)
            {
                if (!string.IsNullOrEmpty(dr[column.ColumnName].ToString()))
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == column.ColumnName)
                        {
                            pro.SetValue(Act, GetValue(dr[column.ColumnName], pro.PropertyType));
                        }
                        else continue;
                    }
                }
                else continue;

            }
            return Act;
        }

        private static Object GetValue(Object DefaultValue, Type type)
        {
            string? Literal = DefaultValue.ToString();
            if (Literal == null || Literal == "" || Literal == string.Empty) return DefaultValue;
            IConvertible obj = Literal;
            Type? u = Nullable.GetUnderlyingType(type);
            if (u != null)
            {
                return (obj == null) ? DefaultValue : Convert.ChangeType(obj, u);
            }
            else
            {
                return Convert.ChangeType(obj, type);
            }
        }
    }
}
