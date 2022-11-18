using Position = QualityManagementApp.Shared.Model.Position;
namespace QualityManagementApp.Client.Services.Catalog;

public class PositionService : IPositionService
{
    private readonly HttpClient _http;
    private readonly ISnackbar _snackbar;
    private readonly ISecurityService _security;

    public PositionService(HttpClient http, ISnackbar snackbar, ISecurityService security)
    {
        _http = http;
        _snackbar = snackbar;
        _security = security;
    }

    public bool IsBusy { get; set; } = false;
    public Position Position { get; set; } = new();
    public List<Position>? Positions { get; set; } = null;

    public async Task AddPosition()
    {
        IsBusy = true;

        Position.Description ??= "";
        var response = await _http.PostAsJsonAsync("api/position/PostPosition", Position);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            await GetPositions();

            var position = await response.Content.ReadFromJsonAsync<Position>();
            await _security.AddLog($"Ha añadido el cargo {position!.Title}");

            _snackbar.Add("El cargo " + position.Title + " fue Añadido.", Severity.Success, config => { config.HideIcon = true; });

            Position = new();
        }

        IsBusy = false;
    }

    public async Task GetPosition(int? positionId)
    {
        IsBusy = true;
        var position = await _http.GetFromJsonAsync<Position>($"api/position/GetPosition/{positionId}");
        Position = position ?? null!;
        IsBusy = false;
    }

    public async Task GetPositions()
    {
        IsBusy = true;
        Positions = await _http.GetFromJsonAsync<List<Position>>("api/position/GetPositions");
        IsBusy = false;
    }

    public async Task DeletePosition()
    {
        IsBusy = true;

        var response = await _http.PostAsJsonAsync("api/position/DeletePosition", Position);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            await _security.AddLog($"Ha eliminado el cargo {Position.Title}");

            _snackbar.Add("El cargo " + Position.Title + " fue Eliminado.", Severity.Error, config => { config.HideIcon = true; });

            Position = new();
            await GetPositions();
        }
        IsBusy = false;
    }

    public async Task<bool> UpdatePosition()
    {
        IsBusy = true;

        Position.Description ??= "";
        var response = await _http.PostAsJsonAsync("api/position/UpdatePosition", Position);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IsBusy = false;
            var position = await response.Content.ReadFromJsonAsync<Position>();
            Position = position!;

            await _security.AddLog($"Ha modificado el cargo {position!.Title}");
            _snackbar.Add("El cargo " + position.Title + " fue Actualizado.", Severity.Info, config => { config.HideIcon = true; });

            await GetPositions();
        }
        IsBusy = false;

        return true;
    }
}
