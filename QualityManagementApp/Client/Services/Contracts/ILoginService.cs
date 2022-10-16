using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Client.Services.Contracts
{
    public interface ILoginService
    {
        /// <value>Enum de los tipos de usuarios</value>
        public enum UserTypeEnum
        {
            Tecnico_Calidad = 1,
            Director = 2
        }

        /// <value>Boolean</value>
        bool IsBusy { get; set; }

        /// <value>Mensaje de respuesta</value>
        string? Message { get; set; }

        /// <value>Valores de un Usuario</value>
        UserDetail User { get; set; }

        /// <summary>
        /// Método para iniciar sesión
        /// </summary>
        Task Login();

        /// <summary>
        /// Método para cerrar sesión
        /// </summary>
        void Logout();
    }
}
