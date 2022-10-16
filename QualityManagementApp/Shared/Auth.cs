using QualityManagementApp.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Shared
{
    public static class Auth
    {
        public static bool VerifyConnection()
        {
            if (SQLConnection.QMA == null)
            {
                SQLConnection.QMA = null!;
                return false;
            }
            return true;
        }

        public static bool StartConnection()
        {
            try
            {
                SQLConnection.StartConnection();
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
