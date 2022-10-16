using Microsoft.AspNetCore.Components;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;
using System.Net.Http.Json;
using QualityManagementApp.Client.Services.Contracts.Catalog;

namespace QualityManagementApp.Client.Services.Catalog
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigation;

        public EmployeeService(HttpClient http, NavigationManager navigation)
        {
            _http = http;
            _navigation = navigation;
        }

        public bool IsBusy { get; set; } = false;
        //public Snackbar Snackbar { get; set; } = new();
        public Employee Employee { get; set; } = new();
        public Employee[]? Employees { get; set; } = null;
        public City[] Cities { get; set; } = Array.Empty<City>();
        public Position[] Positions { get; set; } = Array.Empty<Position>();

        public async Task AddEmployee()
        {
            IsBusy = true;
            Employee.CreationDate = DateTime.Now;
            Employee.LastModificationDate = DateTime.Now;

            var response = await _http.PostAsJsonAsync("api/employee/PostEmployee", Employee);

            //Snackbar.SnackbarIsOpen = true;
            //Snackbar.Message = $"El empleado fue agregado con exito";

            IsBusy = false;
            _navigation.NavigateTo("employees");
        }

        public async Task GetEmployee(string employeeId)
        {
            IsBusy = true;
            var employee = await _http.GetFromJsonAsync<Employee>($"api/employee/GetEmployee/{employeeId}");
            Employee = employee ?? null!;
            IsBusy = false;
        }

        public async Task GetEmployees()
        {
            var jjh = await _http.GetFromJsonAsync<Employee[]>("api/employee/GetEmployees");
            Employees = jjh;
        }

        public async Task GetPositions()
        {
            IsBusy = true;
            Position[]? positions = await _http.GetFromJsonAsync<Position[]?>("api/employee/GetPositions");
            Positions = positions ?? Array.Empty<Position>();
            IsBusy = false;
        }

        public async Task GetCity()
        {
            IsBusy = true;
            City[]? city = await _http.GetFromJsonAsync<City[]?>("api/employee/GetCity");
            Cities = city ?? Array.Empty<City>();
            IsBusy = false;
        }
    }
}

