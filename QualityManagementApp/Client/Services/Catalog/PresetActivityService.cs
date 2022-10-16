using Microsoft.AspNetCore.Components;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;
using System.Net.Http.Json;
using QualityManagementApp.Client.Services.Contracts.Catalog;

namespace QualityManagementApp.Client.Services.Catalog
{
    public class PresetActivityService : IPresetActivityService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigation;

        public PresetActivityService(HttpClient http, NavigationManager navigation)
        {
            _http = http;
            _navigation = navigation;
        }

        public bool IsBusy { get; set; } = false;
        //public Snackbar Snackbar { get; set; } = new();
        public PresetActivity PresetActivity { get; set; } = new();
        public PresetActivity[]? PresetActivities { get; set; } = null;

        public async Task AddPresetActivity()
        {
            IsBusy = true;

            var response = await _http.PostAsJsonAsync("api/presetactivity/PostPresetActivity", PresetActivity);

            //Snackbar.SnackbarIsOpen = true;
            //Snackbar.Message = $"la Actividad fue agregado con exito";

            IsBusy = false;
            _navigation.NavigateTo("presetActivities");
        }

        public async Task GetPresetActivity(string presetActivityId)
        {
            IsBusy = true;
            var presetActivity = await _http.GetFromJsonAsync<PresetActivity>($"api/presetactivity/GetPresetActivity/{presetActivityId}");
            PresetActivity = presetActivity ?? null!;
            IsBusy = false;
        }

        public async Task GetPresetActivities()
        {
            var jjh = await _http.GetFromJsonAsync<PresetActivity[]>("api/presetactivity/GetPresetActivities");
            PresetActivities = jjh;
        }
    }
}
