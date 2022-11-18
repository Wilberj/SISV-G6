using System.Data;
using System.Reflection;
using System.Data.SqlClient;
namespace QualityManagementApp.ADO;

public class GData : GAbstractData
{
    public GData(string ConnectionString)
    {
        QMAConnection = CreateConnection(ConnectionString);
    }

    protected override string BuildDeleteQuery(object obj)
    {
        string TableName = obj.GetType().Name;
        string ConditionString = "";
        Type _type = obj.GetType();
        PropertyInfo[] lst = _type.GetProperties();
        int index = 0;
        foreach (PropertyInfo oProperty in lst)
        {
            string AtributeName = oProperty.Name;
            var AtributeValue = oProperty.GetValue(obj);
            if (AtributeValue != null)
            {
                WhereConstruction(ref ConditionString, ref index, AtributeName, AtributeValue);
            }
        }
        ConditionString = ConditionString.TrimEnd(new char[] { '0', 'R' });
        string strQuery = "DELETE FROM " + TableName + ConditionString;
        return strQuery;
    }

    protected override string BuildInsertQueryByObject(object obj, List<string>? notMapped = null)
    {
        string ColumnNames = "";
        string Values = "";
        Type _type = obj.GetType();
        PropertyInfo[] lst = _type.GetProperties();
        List<EntityProps> entityProps = DescribeEntity(obj.GetType().Name);
        notMapped ??= new() { "" };

        foreach (PropertyInfo oproperty in lst)
        {
            string AtributeName = oproperty.Name;
            if (AtributeName != (notMapped!.Find(i => i == AtributeName) ?? ""))
            {
                var AtributeValue = oproperty.GetValue(obj);
                var entityProp = entityProps!.Find(e => e.COLUMN_NAME == AtributeName);
                if (AtributeValue != null && entityProps != null)
                {
                    switch (entityProp!.DATA_TYPE)
                    {
                        case "nvarchar":
                        case "varchar":
                        case "text":
                        case "char":
                            ColumnNames += AtributeName.ToString() + ",";
                            Values = Values + "'" + AtributeValue.ToString() + "',";
                            break;

                        case "int":
                        case "float":
                        case "decimal":
                        case "bigint":
                        case "money":
                        case "smallint":
                            ColumnNames += AtributeName.ToString() + ",";
                            Values += AtributeValue.ToString() + ",";
                            break;
                        case "bit":
                            ColumnNames += AtributeName.ToString() + ",";
                            Values = Values + (AtributeValue.ToString() == "True" ? "1" : "0") + ",";
                            break;
                        case "date":
                            ColumnNames += AtributeName.ToString() + ",";
                            Values = Values + "'" + ((DateTime)AtributeValue).ToString("dd/MM/yyyy") + "',";
                            break;
                        case "datetime":
                            ColumnNames += AtributeName.ToString() + ",";
                            Values = Values + "'" + ((DateTime)AtributeValue).ToString("dd/MM/yyyy hh:mm:ss tt") + "',";
                            break;
                    }
                }
                else continue;
            }
        }
        ColumnNames = ColumnNames.TrimEnd(',');
        Values = Values.TrimEnd(',');
        return "INSERT INTO " + obj.GetType().Name + "( " + ColumnNames + " ) VALUES (" + Values + ") SELECT SCOPE_IDENTITY()";
    }

    protected override string BuildUpdateQueryByObject(object obj, string WhereProp)
    {
        string TableName = obj.GetType().Name;
        string Values = "";
        string Condition = "";
        Type _type = obj.GetType();
        PropertyInfo[] lst = _type.GetProperties();
        List<EntityProps> entityProps = DescribeEntity(obj.GetType().Name);
        int index = 0;
        foreach (PropertyInfo oproperty in lst)
        {
            string AtributeName = oproperty.Name;
            var AtributeValue = oproperty.GetValue(obj);
            var EntityProp = entityProps.Find(e => e.COLUMN_NAME == AtributeName);
            if (AtributeValue != null && EntityProp != null)
            {
                if (WhereProp != AtributeName)
                {
                    Values = BuildSetForUpdate(Values, AtributeName, AtributeValue, EntityProp);
                }
                else WhereConstruction(ref Condition, ref index, AtributeName, AtributeValue);
            }
            else continue;
        }
        Values = Values.TrimEnd(',');
        string strQuery = "UPDATE " + TableName + " SET " + Values + Condition;
        return strQuery;
    }

    protected override string BuildUpdateQueryByObject(object obj, string[] WhereProps)
    {
        string TableName = obj.GetType().Name;
        string Values = "";
        string Conditions = "";
        Type _type = obj.GetType();
        PropertyInfo[] lst = _type.GetProperties();
        List<EntityProps> entityProps = DescribeEntity(obj.GetType().Name);
        int index = 0;
        foreach (PropertyInfo oproperty in lst)
        {
            string AtributeName = oproperty.Name;
            var AtributeValue = oproperty.GetValue(obj);
            var EntityProp = entityProps.Find(e => e.COLUMN_NAME == AtributeName);
            if (AtributeValue != null && EntityProp != null)
            {
                if ((from O in WhereProps where O == AtributeName select O).ToList().Count == 0)
                {
                    Values = BuildSetForUpdate(Values, AtributeName, AtributeValue, EntityProp);

                }
                else WhereConstruction(ref Conditions, ref index, AtributeName, AtributeValue);
            }
            else continue;
        }
        Values = Values.TrimEnd(',');
        string strQuery = "UPDATE " + TableName + " SET " + Values + Conditions;
        return strQuery;
    }

    protected override IDbConnection CreateConnection(string connection)
    {
        return new SqlConnection(connection);
    }

    protected override IDataAdapter CreateDataAdapterSQL(string SQLcommand, IDbConnection connection)
    {
        var da = new SqlDataAdapter((SqlCommand)SQLCommand(SQLcommand, connection));
        return da;
    }

    protected override IDataAdapter CreateDataAdapterSQL(IDbCommand SQLCommand)
    {
        var da = new SqlDataAdapter((SqlCommand)SQLCommand);
        return da;
    }

    protected override List<EntityProps> DescribeEntity(string entityName)
    {
        string DescribeEntity = @"SELECT COLUMN_NAME, IS_NULLABLE, DATA_TYPE
                                      from [INFORMATION_SCHEMA].[COLUMNS]
                                      Where [TABLE_NAME] = '" + entityName
                                  + "' order by [ORDINAL_POSITION]";
        DataTable Table = GetDataSQL(DescribeEntity);
        return ConvertDataTable<EntityProps>(Table, new EntityProps());
    }

    protected override IDbCommand SQLCommand(string SQLcommand, IDbConnection connection)
    {
        var com = new SqlCommand(SQLcommand, (SqlConnection)connection);
        return com;
    }

    private protected override DataTable BuildTable(object obj, ref string condSQL, List<string>? notMapped = null)
    {
        string ConditionString = "";
        string Columns = "";
        Type _type = obj.GetType();
        PropertyInfo[] lst = _type.GetProperties();
        int index = 0;
        List<EntityProps> entityProps = DescribeEntity(obj.GetType().Name);
        notMapped ??= new() { "" };

        foreach (PropertyInfo oProperty in lst)
        {
            string AtributeName = oProperty.Name;

            if (AtributeName != (notMapped!.Find(i => i == AtributeName) ?? ""))
            {
                var EntityProp = entityProps!.Find(e => e.COLUMN_NAME == AtributeName);
                if (entityProps != null)
                {
                    var AtributeValue = oProperty.GetValue(obj);
                    Columns = Columns + AtributeName + ",";
                    if (AtributeValue != null)
                    {
                        WhereConstruction(ref ConditionString, ref index, AtributeName, AtributeValue);
                    }
                }
            }
        }
        ConditionString = ConditionString.TrimEnd(new char[] { '0', 'R' });
        if (ConditionString == "" && condSQL != "" && condSQL != null)
        {
            ConditionString = " WHERE ";

        }
        else if (ConditionString != "" && condSQL != "")
        {
            ConditionString += " AND ";
        }
        Columns = Columns.TrimEnd(',');
        string queryString = "SELECT " + Columns + " FROM " + obj.GetType().Name + ConditionString + condSQL;
        DataTable Table = GetDataSQL(queryString);
        return Table;
    }

    private static string BuildSetForUpdate(string Values, string AtributeName, object AtributeValue, EntityProps entityprop)
    {
        switch (entityprop.DATA_TYPE)
        {
            case "nvarchar":
            case "varchar":
            case "char":
            case "text":
                Values = Values + AtributeName + " = '" + AtributeValue.ToString() + "',";
                break;

            case "int":
            case "float":
            case "decimal":
            case "bigint":
            case "money":
            case "smallint":
                Values = Values + AtributeName + " = " + AtributeValue.ToString() + ",";
                break;
            case "bit":
                Values = Values + AtributeName + " = " + (AtributeValue.ToString() == "True" ? "1" : "0") + ",";
                break;
            case "date":
                Values = Values + AtributeName + "= '" + ((DateTime)AtributeValue).ToString("dd/MM/yyyy") + "',";
                break;
            case "datetime":
                Values = Values + AtributeName + "= '" + ((DateTime)AtributeValue).ToString("dd/MM/yyyy hh:mm:ss tt") + "',";
                break;
        }
        return Values;
    }

    private static void WhereConstruction(ref string ConditionString, ref int index, string AtributeName, object AtributeValue)
    {
        if (AtributeValue.GetType() == typeof(string) && AtributeValue.ToString()!.Length < 200)
        {
            WhereOrAdd(ref ConditionString, ref index);
            ConditionString = ConditionString + AtributeName + " = '" + AtributeValue.ToString() + "' ";
            //ConditionString = ConditionString + AtributeName + " LIKE '%" + AtributeValue.ToString() + "%' ";
        }
        else if (AtributeValue.GetType() == typeof(DateTime))
        {
            WhereOrAdd(ref ConditionString, ref index);
            ConditionString = ConditionString + AtributeName + " = '" + ((DateTime)AtributeValue).ToString("yyyy/MM/dd") + "' ";
        }
        else if (AtributeValue.GetType() == typeof(int)
            || AtributeValue.GetType() == typeof(Double)
            || AtributeValue.GetType() == typeof(Decimal)
            || AtributeValue.GetType() == typeof(int?))
        {
            WhereOrAdd(ref ConditionString, ref index);
            ConditionString = ConditionString + AtributeName + " = " + AtributeValue.ToString();
        }
    }

    private static void WhereOrAdd(ref string ConditionString, ref int index)
    {
        if (index == 0)
        {
            ConditionString = " WHERE ";
            index++;
        }
        else
        {
            ConditionString += " AND ";
        }
    }
}
