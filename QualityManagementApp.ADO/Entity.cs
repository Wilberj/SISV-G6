using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityManagementApp.ADO
{
    public class Entity
    {
        public List<T> Get<T>()
        {
            var Data = SQLConnection.QMA.TakeList<T>(this);
            return Data;
        }

        public T? FindObject<T>()
        {
            var Data = SQLConnection.QMA.TakeObject<T>(this);
            return Data ?? default;
        }

        public List<T> Get<T>(string condition)
        {
            var Data = SQLConnection.QMA.TakeList<T>(this, condition);
            return Data;
        }

        //   Inicio prueba
        public static T GetProcedure<T>(string procedure, Object obj, List<Object> parameters)
        {
            var Data = SQLConnection.QMA.ExecuteSqlProcedure<T>(procedure, obj ,parameters);
            return Data;
        }
        //   Fin prueba

        public object Save()
        {
            return SQLConnection.QMA.InsertObject(this);
        }

        public bool Update(string ID)
        {
            SQLConnection.QMA.UpdateObject(this, ID);
            return true;
        }

        public bool Update(string[] ID)
        {
            SQLConnection.QMA.UpdateObject(this, ID);
            return true;
        }

        public bool Delete()
        {
            SQLConnection.QMA.Delete(this);
            return true;
        }
    }
}
