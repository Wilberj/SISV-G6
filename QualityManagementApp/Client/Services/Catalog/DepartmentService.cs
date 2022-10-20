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

        public DepartmentService(HttpClient http)
        {
            _http = http;
        }

        public bool IsBusy { get; set; } = false;
        //public Snackbar Snackbar { get; set; } = new();
        public Department Department { get; set; } = new();
        public Department[]? Departments { get; set; } = null;

        public async Task AddDepartment()
        {
            IsBusy = true;

            await _http.PostAsJsonAsync("api/department/PostDepartment", Department);
            await GetDepartments();
            Department = new();
            //Snackbar.SnackbarIsOpen = true;
            //Snackbar.Message = $"El Departamento fue agregado con exito";

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
            var departments = await _http.GetFromJsonAsync<Department[]>("api/department/GetDepartments");
            Departments = departments!;
        }
    }
}
