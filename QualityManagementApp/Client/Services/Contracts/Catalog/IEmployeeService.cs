namespace QualityManagementApp.Client.Services.Contracts.Catalog;

public interface IEmployeeService
{
    /// <value>Boolean</value>
    bool IsBusy { get; set; }

    /// <value>Valores de un empleado</value>
    Employee Employee { get; set; }

    /// <value>Valores de todos los empleados</value>
    List<Employee>? Employees { get; set; }

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
    /// Método que elimina una ciudad
    /// </summary>
    Task DeleteEmployee();

    /// <summary>
    /// Método que actualiza una ciudad
    /// </summary>
    Task<bool> UpdateEmployee();
}
