using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Client.Services.Contracts.Catalog
{
    public interface ICityService
    {
        /// <value>Boolean</value>
        bool IsBusy { get; set; }

        /// <value>Valores de un Snackbar</value>
        //Snackbar Snackbar { get; set; }

        /// <value>Valores de una ciudad</value>
        City City { get; set; }

        /// <value>Valores de todas las ciudades</value>
        City[]? Cities { get; set; }

        /// <value>Valores de todos los departamentos</value>
        Department[] Departments { get; set; }


        /// <summary>
        /// Método para guardar una ciudad
        /// </summary>
        Task AddCity();

        /// <summary>
        /// Método que obtiene una ciduad
        /// </summary>
        Task GetCity(string cityId);

        /// <summary>
        /// Método que obtiene todas las ciudades
        /// </summary>
        Task GetCities();

        /// <summary>
        /// Método que obtiene todos los departamentos
        /// </summary>
        Task GetDepartments();
    }
}
