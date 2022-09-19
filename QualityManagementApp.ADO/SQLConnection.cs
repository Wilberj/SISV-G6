using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityManagementApp.ADO
{
    public class SQLConnection
    {
        private static string UserSQLConection = "";
        #pragma warning disable CA2211 // Los campos no constantes no deben ser visibles
        public static GData QMA = null!;
        #pragma warning restore CA2211 // Los campos no constantes no deben ser visibles
        static readonly string DataBaseName = "QMA";
        static readonly string Server = "JUNIOR";

        static public bool StartConnection(string user, string password)
        {
            try
            {
                UserSQLConection = "Data Source=" + Server +
                    "; Initial Catalog=" + DataBaseName + "; User ID=" +
                    user + ";Password=" + password + ";Trusted_Connection=True;";
                QMA = new GData(UserSQLConection);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
