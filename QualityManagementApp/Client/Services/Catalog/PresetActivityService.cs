namespace QualityManagementApp.Client.Services.Catalog;

public class PresetActivityService : IPresetActivityService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigation;
    private readonly ISnackbar _snackbar;
    private readonly ISecurityService _security;

    public PresetActivityService(HttpClient http, NavigationManager navigation, ISnackbar snackbar, ISecurityService security)
    {
        _http = http;
        _navigation = navigation;
        _snackbar = snackbar;
        _security = security;
    }

    public bool IsBusy { get; set; } = false;
    public PresetActivity PresetActivity { get; set; } = new();
    public List<PresetActivity>? PresetActivities { get; set; } = null;

    public async Task AddPresetActivity()
    {
        IsBusy = true;
        PresetActivity.Description ??= "";

        var response = await _http.PostAsJsonAsync("api/presetactivity/PostPresetActivity", PresetActivity);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            await GetPresetActivities();

            var preset = await response.Content.ReadFromJsonAsync<PresetActivity>();
            await _security.AddLog($"Ha añadido la actividad preestablecida {preset!.Title}");

            _snackbar.Add("La actividad preestablecida " + preset.Title + " fue Añadida.", Severity.Success, config => { config.HideIcon = true; });

            _navigation.NavigateTo("presetActivities");
        }
        IsBusy = false;
    }

    public async Task GetPresetActivity(int? presetActivityId)
    {
        IsBusy = true;
        var presetActivity = await _http.GetFromJsonAsync<PresetActivity>($"api/presetactivity/GetPresetActivity/{presetActivityId}");
        PresetActivity = presetActivity ?? null!;
        IsBusy = false;
    }

    public async Task GetPresetActivities()
    {
        IsBusy = true;
        PresetActivities = await _http.GetFromJsonAsync<List<PresetActivity>>("api/presetactivity/GetPresetActivities");
        IsBusy = false;
    }

    public async Task DeletePresetActivity()
    {
        IsBusy = true;

        var response = await _http.PostAsJsonAsync("api/presetactivity/DeletePresetActivity", PresetActivity);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            await _security.AddLog($"Ha eliminado la actividad preestablecida {PresetActivity.Title}");

            _snackbar.Add("La actividad preestablecida " + PresetActivity.Title + " fue Eliminada.", Severity.Error, config => { config.HideIcon = true; });

            await GetPresetActivities();
            _navigation.NavigateTo("presetActivities");
        }
        IsBusy = false;

    }

    public async Task<bool> UpdatePresetActivity()
    {
        IsBusy = true; 
        PresetActivity.Description ??= "";

        var response = await _http.PostAsJsonAsync("api/presetactivity/UpdatePresetActivity", PresetActivity);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            var preset = await response.Content.ReadFromJsonAsync<PresetActivity>();
            PresetActivity = preset!;

            await _security.AddLog($"Ha modificado la actividad preestablecida {preset!.Title}");
            _snackbar.Add("La actividad preestablecida " + preset.Title + " fue Actualizada.", Severity.Info, config => { config.HideIcon = true; });
        }
        IsBusy = false;

        return true;
    }
}
