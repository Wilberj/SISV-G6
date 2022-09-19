using QualityManagementApp.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Shared
{
    public class Auth
    {
        #pragma warning disable CA2211 // Los campos no constantes no deben ser visibles
        static public UserModel? User;
        #pragma warning restore CA2211 // Los campos no constantes no deben ser visibles
        static public bool VerifyAuthenticate()
        {
            if (SQLConnection.QMA == null)
            {
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
                SQLConnection.QMA = null;
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
                return false;
            }
            return true;
        }

        static public bool LoginIn(string user, string password)
        {
            try
            {
                SQLConnection.StartConnection(user, password);
                User = new UserModel(new UserDetail() { Username = user, Password = password }.FindObject<UserDetail>())
                {
                    User = user
                };
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        static public bool ClearSeason()
        {
            return true;
        }
    }

    public class UserModel
    {
        public UserModel(UserDetail tableuser)
        {
            this.User = tableuser.Username;
            this.Success = true;
            this.Pkuser = tableuser.PkUser;
            this.FKTypeUser = tableuser.FkUserType;
            //this.Permission = new List<String> { "DIRECTOR", "TECNICO DE CALDIAD", "ADMINISTRADOR" };
        }

        public string User { get; set; }
        public bool Success { get; set; }
        public int? Pkuser { get; set; }
        public int? FKTypeUser { get; set; }
        //public List<String> Permission { get; set; }
    }
}
