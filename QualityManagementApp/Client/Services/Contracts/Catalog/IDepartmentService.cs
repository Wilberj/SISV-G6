namespace QualityManagementApp.Client.Services.Contracts.Catalog;

public interface IDepartmentService
{
    /// <value>Boolean</value>
    bool IsBusy { get; set; }

    /// <value>Valores de un departamento</value>
    Department Department { get; set; }

    /// <value>Valores de todos los departamentos</value>
    List<Department>? Departments { get; set; }


    /// <summary>
    /// Método para guardar un departamento
    /// </summary>
    Task AddDepartment();

    /// <summary>
    /// Método que obtiene un departamento
    /// </summary>
    Task GetDepartment(int? departmentId);

    /// <summary>
    /// Método que obtiene todos los departamentos
    /// </summary>
    Task GetDepartments();

    /// <summary>
    /// Método que elimina un departamento
    /// </summary>
    Task DeleteDepartment();

    /// <summary>
    /// Método que actualiza un departamento
    /// </summary>
    Task<bool> UpdateDepartment();
}

