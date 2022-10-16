using Microsoft.AspNetCore.Components;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;
using System.Net.Http.Json;
using QualityManagementApp.Client.Services.Contracts.Catalog;

namespace QualityManagementApp.Client.Services.Catalog
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigation;

        public DepartmentService(HttpClient http, NavigationManager navigation)
        {
            _http = http;
            _navigation = navigation;
        }

        public bool IsBusy { get; set; } = false;
        //public Snackbar Snackbar { get; set; } = new();
        public Department Department { get; set; } = new();
        public Department[]? Departments { get; set; } = null;

        public async Task AddDepartment()
        {
            IsBusy = true;

            var response = await _http.PostAsJsonAsync("api/department/PostDepartment", Department);

            //Snackbar.SnackbarIsOpen = true;
            //Snackbar.Message = $"El Departamento fue agregado con exito";

            IsBusy = false;
            _navigation.NavigateTo("departments");
        }

        public async Task GetDepartment(string departmentId)
        {
            IsBusy = true;
            var department = await _http.GetFromJsonAsync<Department>($"api/department/GetDepartment/{departmentId}");
            Department = department ?? null!;
            IsBusy = false;
        }

        public async Task GetDepartments()
        {
            var jjh = await _http.GetFromJsonAsync<Department[]>("api/department/GetDepartments");
            Departments = jjh;
        }
    }
}
