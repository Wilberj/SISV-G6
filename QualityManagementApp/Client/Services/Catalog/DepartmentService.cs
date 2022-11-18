namespace QualityManagementApp.Client.Services.Catalog;

public class DepartmentService : IDepartmentService
{
    private readonly HttpClient _http;
    private readonly ISnackbar _snackbar;
    private readonly ISecurityService _security;

    public DepartmentService(HttpClient http, ISnackbar snackbar, ISecurityService security)
    {
        _http = http;
        _snackbar = snackbar;
        _security = security;
    }

    public bool IsBusy { get; set; } = false;
    public Department Department { get; set; } = new();
    public List<Department>? Departments { get; set; } = null;

    public async Task AddDepartment()
    {
        IsBusy = true;

        var response = await _http.PostAsJsonAsync("api/department/PostDepartment", Department);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            await GetDepartments();
            Department = new();

            var department = await response.Content.ReadFromJsonAsync<Department>();
            await _security.AddLog($"Ha añadido el departamento {department!.Name}");

            _snackbar.Add("El departamento " + department!.Name + " fue Añadido.", Severity.Success, config => { config.HideIcon = true; });
        }

        IsBusy = false;
    }

    public async Task GetDepartment(int? departmentId)
    {
        IsBusy = true;
        var department = await _http.GetFromJsonAsync<Department>($"api/department/GetDepartment/{departmentId}");
        Department = department!;
        IsBusy = false;
    }

    public async Task GetDepartments()
    {
        IsBusy = true;
        Departments = await _http.GetFromJsonAsync<List<Department>>("api/department/GetDepartments");
        IsBusy = false;
    }

    public async Task DeleteDepartment()
    {
        IsBusy = true;

        var response = await _http.PostAsJsonAsync("api/department/DeleteDepartment", Department);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            await _security.AddLog($"Ha eliminado el departamento {Department!.Name}");

            _snackbar.Add("El departamento " + Department!.Name + " fue Eliminado.", Severity.Error, config => { config.HideIcon = true; });
            Department = new();
            await GetDepartments();
        }
        IsBusy = false;
    }

    public async Task<bool> UpdateDepartment()
    {
        IsBusy = true;
        var response = await _http.PostAsJsonAsync("api/department/UpdateDepartment", Department);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            var department = await response.Content.ReadFromJsonAsync<Department>();
            Department = department!;

            await _security.AddLog($"Ha modificado el departamento {Department.Name}");
            _snackbar.Add("El departamento " + Department.Name + " fue Actualizado.", Severity.Info, config => { config.HideIcon = true; });
            await GetDepartments();
        }

        IsBusy = false;
        return true;
    }
}
