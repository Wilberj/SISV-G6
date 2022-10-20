using Microsoft.AspNetCore.Components;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;
using System.Net.Http.Json;
using QualityManagementApp.Client.Services.Contracts.Catalog;

namespace QualityManagementApp.Client.Services.Catalog
{
    public class CityService : ICityService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigation;

        public CityService(HttpClient http, NavigationManager navigation)
        {
            _http = http;
            _navigation = navigation;
        }

        public bool IsBusy { get; set; } = false;
        //public Snackbar Snackbar { get; set; } = new();
        public City City { get; set; } = new();
        public City[]? Cities { get; set; } = null;

        public async Task AddCity()
        {
            IsBusy = true;

            await _http.PostAsJsonAsync("api/city/PostCity", City);

            //Snackbar.SnackbarIsOpen = true;
            //Snackbar.Message = $"La ciduad fue agregada con exito";

            IsBusy = false;
            _navigation.NavigateTo("cities");
        }

        public async Task GetCity(int? cityId)
        {
            IsBusy = true;
            var city = await _http.GetFromJsonAsync<City>($"api/city/GetCity/{cityId}");
            City = city!;
            IsBusy = false;
        }

        public async Task GetCities()
        {
            var jjh = await _http.GetFromJsonAsync<City[]>("api/city/GetCities");
            Cities = jjh;
        }
    }
}
