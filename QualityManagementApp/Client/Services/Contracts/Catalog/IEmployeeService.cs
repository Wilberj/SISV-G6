using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Client.Services.Contracts.Catalog
{
    public interface IEmployeeService
    {
        /// <value>Boolean</value>
        bool IsBusy { get; set; }

        /// <value>Valores de un Snackbar</value>
        //Snackbar Snackbar { get; set; }

        /// <value>Valores de un empleado</value>
        Employee Employee { get; set; }

        /// <value>Valores de todos los empleados</value>
        Employee[]? Employees { get; set; }

        /// <value>Valores de todas las ciudades</value>
        City[] Cities { get; set; }

        /// <value>Valores de todos los cargos</value>
        Position[] Positions { get; set; }

        /// <summary>
        /// Método para guardar un empleado
        /// </summary>
        Task AddEmployee();

        /// <summary>
        /// Método que obtiene un empleado
        /// </summary>
        Task GetEmployee(int? EmployeeId);

        /// <summary>
        /// Método que obtiene todos los empleados
        /// </summary>
        Task GetEmployees();

        /// <summary>
        /// Método que obtiene todos los cargos
        /// </summary>
        Task GetPositions();

        /// <summary>
        /// Método que obtiene todas las ciudades
        /// </summary>
        Task GetCity();
    }
}
