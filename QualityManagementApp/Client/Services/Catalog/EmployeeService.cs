using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Client.Services.Catalog;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigation;
    private readonly ISnackbar _snackbar;
    private readonly ISecurityService _security;

    public EmployeeService(HttpClient http, NavigationManager navigation, ISnackbar snackbar, ISecurityService security)
    {
        _http = http;
        _navigation = navigation;
        _snackbar = snackbar;
        _security = security;
    }

    public bool IsBusy { get; set; } = false;
    public Employee Employee { get; set; } = new();
    public List<Employee>? Employees { get; set; } = null;

    public async Task AddEmployee()
    {
        IsBusy = true;

        ChangeValuesNull();
        Employee.CreationDate = DateTime.Now;
        Employee.LastModificationDate = DateTime.Now;

        var response = await _http.PostAsJsonAsync("api/employee/PostEmployee", Employee);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            await GetEmployees();

            var employee = await response.Content.ReadFromJsonAsync<Employee>();
            await _security.AddLog($"Ha añadido al empleado {employee!.FirstName} {employee.SecondName}");

            _snackbar.Add("El Empleado " + employee.FirstName + " " + employee.SecondName + " fue Añadido.", Severity.Success, config => { config.HideIcon = true; });

            _navigation.NavigateTo("employees");
        }
        IsBusy = false;
    }

    public async Task GetEmployee(int? employeeId)
    {
        IsBusy = true;
        var employee = await _http.GetFromJsonAsync<Employee>($"api/employee/GetEmployee/{employeeId}");
        Employee = employee ?? null!;
        IsBusy = false;
    }

    public async Task GetEmployees()
    {
        IsBusy = true;
        Employees = await _http.GetFromJsonAsync<List<Employee>>("api/employee/GetEmployees");
        IsBusy = false;
    }

    public async Task DeleteEmployee()
    {
        IsBusy = true;

        var response = await _http.PostAsJsonAsync("api/employee/DeleteEmployee", Employee);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            await _security.AddLog($"Ha eliminado al empleado {Employee.FirstName} {Employee.SecondName}");

            _snackbar.Add("El empleado " + Employee.FirstName + " fue Eliminado.", Severity.Error, config => { config.HideIcon = true; });
            _navigation.NavigateTo("employees");
            await GetEmployees();
        }
        IsBusy = false;
    }

    public async Task<bool> UpdateEmployee()
    {
        IsBusy = true;
        ChangeValuesNull();
        Employee.LastModificationDate = DateTime.Now;
        var response = await _http.PostAsJsonAsync("api/employee/UpdateEmployee", Employee);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            var employee = await response.Content.ReadFromJsonAsync<Employee>();
            Employee = employee!;

            await _security.AddLog($"Ha modificado al empleado {employee!.FirstName} {employee!.SecondName}");
            _snackbar.Add("El empleado " + employee.FirstName + " fue Actualizado.", Severity.Info, config => { config.HideIcon = true; });
        }
        IsBusy = false;

        return true;
    }

    void ChangeValuesNull()
    {
        Employee.SecondName ??= "";
        Employee.SecondSurname ??= "";
        Employee.HomeAddress ??= "";
        Employee.Telephone ??= "";
    }
}
