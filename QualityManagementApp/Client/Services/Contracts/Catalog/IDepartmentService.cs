using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Client.Services.Contracts.Catalog
{
    public interface IDepartmentService
    {
        /// <value>Boolean</value>
        bool IsBusy { get; set; }

        /// <value>Valores de un Snackbar</value>
        //Snackbar Snackbar { get; set; }

        /// <value>Valores de un departamento</value>
        Department Department { get; set; }

        /// <value>Valores de todos los departamentos</value>
        Department[]? Departments { get; set; }


        /// <summary>
        /// Método para guardar un departamento
        /// </summary>
        Task AddDepartment();

        /// <summary>
        /// Método que obtiene un departamento
        /// </summary>
        Task GetDepartment(string departmentId);

        /// <summary>
        /// Método que obtiene todos los departamentos
        /// </summary>
        Task GetDepartments();
    }
}

