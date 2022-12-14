namespace QualityManagementApp.Client.Services;

public class PlanningService : IPlanningService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigation;
    private readonly ISnackbar _snackbar;
    private readonly ISecurityService _security;

    public PlanningService(HttpClient http, NavigationManager navigation, ISnackbar snackbar, ISecurityService security)
    {
        _http = http;
        _navigation = navigation;
        _snackbar = snackbar;
        _security = security;
    }

    public Planning Planning { get; set; } = new();
    public bool IsBusy { get; set; }
    public List<Planning>? Plannings { get; set; } = null;
    public Activity Activity { get; set; } = new();
    public List<Status> Statuses { get; set; } = new();

    public async Task GetPlannings()
    {
        IsBusy = true;
        Plannings = await _http.GetFromJsonAsync<List<Planning>>("api/Planning/GetPlannings");
        IsBusy = false;
    }

    public async Task GetPlanning(int planningId)
    {
        IsBusy = true;
        var planning = await _http.GetFromJsonAsync<Planning>($"api/planning/GetPlanning/{planningId}");
        Planning = planning!;
        IsBusy = false;
    }

    public async Task GetActivity(int activityId)
    {
        IsBusy = true;
        var activity = await _http.GetFromJsonAsync<Activity>($"api/planning/GetActivity/{activityId}");
        Activity = activity!;
        IsBusy = false;
    }

    public async Task GetStatuses()
    {
        IsBusy = true;
        var statuses = await _http.GetFromJsonAsync<List<Status>>($"api/planning/GetStatuses");
        Statuses = statuses!;
        IsBusy = false;
    }

    public async Task AddPlanning()
    {
        if (Planning.Activities!.Capacity != 0)
        {
            IsBusy = true;

            Planning.FkStatus = (int?)(Planning.IsVerified == true ? IPlanningService.Status.En_Progreso : IPlanningService.Status.Indefinido);
            Planning.CreationDate = DateTime.Now;
            Planning.LastModificationDate = DateTime.Now;

            var response = await _http.PostAsJsonAsync("api/planning/PostPlanning", Planning);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IsBusy = false;
                var planning = await response.Content.ReadFromJsonAsync<Planning>();
                await _security.AddLog($"Ha añadido la Planificación {planning!.Title}");

                _snackbar.Add("Planificación " + planning!.Title + " Añadida.", Severity.Success, config => { config.HideIcon = true; });
                _navigation.NavigateTo("plannings");
            }

            IsBusy = false;
        }
    }

    public void AddActivity()
    {
        if (Activity.Details!.Capacity != 0)
        {
            IsBusy = true;
            Activity.CreationDate = DateTime.Now;
            Activity.LastModificationDate = DateTime.Now;
            Activity.FkStatus = (int)IPlanningService.Status.Indefinido;
            Activity.ActivityId = Planning.Activities!.Count + 1;

            Planning.Activities!.Add(Activity);
            Activity = new()
            {
                QualityExpected = 1,
                Details = new()
            };
            IsBusy = false;
        }
    }

    public async Task UpdateActivity()
    {
        IsBusy = true;
        Activity.LastModificationDate = DateTime.Now;
        Activity.FkStatus = (int)IPlanningService.Status.Completada;
        Activity.FinishDate = DateTime.Now;

        var response = await _http.PostAsJsonAsync("api/planning/UpdateActivity", Activity);

        IsBusy = false;
        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            _snackbar.Add("La actividad predecesora no se ha completado", Severity.Error, config => { config.HideIcon = true; });
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            await _security.AddLog($"Ha actualizado la Actividad {Activity.Title}");
            await GetPlanning((int)Planning.PkPlanning!);

            _snackbar.Add("Actividad " + Activity.Title + " Actualizada.", Severity.Success, config => { config.HideIcon = true; });
        }
    }

    public async Task UpdateActivityStatus()
    {
        Activity.LastModificationDate = DateTime.Now;
        Activity.QualityObtained = null;
        await _http.PostAsJsonAsync("api/planning/UpdateActivityStatus", Activity);
        await _security.AddLog($"Ha actualizado el estado de la Actividad {Activity.Title}");
        _snackbar.Add("Ha actualizado el estado de la Actividad " + Activity.Title, Severity.Success, config => { config.HideIcon = true; });
    }

    public async Task UpdatePlanningStatus(bool? check)
    {
        Planning.IsVerified = check;
        Planning.FkStatus = (int?)(Planning.IsVerified == true ? IPlanningService.Status.En_Progreso : IPlanningService.Status.Indefinido);
        Planning.LastModificationDate = DateTime.Now;

        await _http.PostAsJsonAsync("api/planning/UpdatePlanningStatus", Planning);
        await _security.AddLog($"Ha actualizado el estado de la Planificación {Planning.Title}");
    }
}
