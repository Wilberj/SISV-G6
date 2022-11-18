namespace QualityManagementApp.Client.Services.Catalog;

public class CityService : ICityService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigation;
    private readonly ISnackbar _snackbar;
    private readonly ISecurityService _security;

    public CityService(HttpClient http, NavigationManager navigation, ISnackbar snackbar, ISecurityService security)
    {
        _http = http;
        _navigation = navigation;
        _snackbar = snackbar;
        _security = security;
    }

    public bool IsBusy { get; set; } = false;
    public City City { get; set; } = new();
    public List<City>? Cities { get; set; } = null;

    public async Task AddCity()
    {
        IsBusy = true;

        var response = await _http.PostAsJsonAsync("api/city/PostCity", City);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            var city = await response.Content.ReadFromJsonAsync<City>();
            await _security.AddLog($"Ha añadido la ciudad {city!.Name}");

            _snackbar.Add("La ciudad " + city!.Name + " fue Añadida.", Severity.Success, config => { config.HideIcon = true; });
            _navigation.NavigateTo("cities");
        }

        IsBusy = false;        
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
        IsBusy = true;
        Cities = await _http.GetFromJsonAsync<List<City>>("api/city/GetCities");
        IsBusy = false;
    }

    public async Task DeleteCity()
    {
        IsBusy = true;

        var response = await _http.PostAsJsonAsync("api/city/DeleteCity", City);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            await _security.AddLog($"Ha eliminado la ciudad {City!.Name}");

            _snackbar.Add("La ciudad " + City!.Name + " fue Eliminada.", Severity.Error, config => { config.HideIcon = true; });
            _navigation.NavigateTo("cities");
        }
        IsBusy = false;     
    }

    public async Task<bool> UpdateCity()
    {
        IsBusy = true;
        var response = await _http.PostAsJsonAsync("api/city/UpdateCity", City);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            var city = await response.Content.ReadFromJsonAsync<City>();
            City = city!;

            await _security.AddLog($"Ha modificado la ciudad {City!.Name}");
            _snackbar.Add("La ciudad " + City!.Name + " fue Actualizada.", Severity.Info, config => { config.HideIcon = true; });
        }
        IsBusy = false;

        return true;
    }
}
