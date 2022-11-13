namespace QualityManagementApp.Client.Services;

public class SecurityService : ISecurityService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigation;

    public SecurityService(HttpClient http, NavigationManager navigation)
    {
        _http = http;
        _navigation = navigation;
    }

    public bool IsBusy { get; set; }
    public string? Message { get; set; } = null;
    public UserDetail User { get; set; } = new();
    public List<UserLog>? Logs { get; set; } = null;

    public async Task Login()
    {
        IsBusy = true;
        Message = null;
        var responseUser = await _http.PostAsJsonAsync("api/security/Login", User);

        User = new();
        LoginData.UserLogin = await responseUser.Content.ReadFromJsonAsync<UserModel>();

        var data = LoginData.UserLogin ?? new();
        if (data.Username != null)
        {
            IsBusy = false;
            _navigation.NavigateTo("/surveys");
        }
        else
        {
            Message = "Credenciales incorrectas.";
            IsBusy = false;
        }
    }

    public void Logout()
    {
        User = new();
        LoginData.UserLogin = null;
        _navigation.NavigateTo("/");
    }

    public async Task AddLog(string message)
    {
        UserLog log = new()
        {
            CreationDate = DateTime.Now,
            FkUser = LoginData.UserLogin!.Id,
            Description = message
        };

        await _http.PostAsJsonAsync("api/security/PostLog", log);
    }

    public async Task GetLogs()
    {
        IsBusy = true;
        Logs = await _http.GetFromJsonAsync<List<UserLog>>($"api/security/GetLogs/{LoginData.UserLogin!.Id}");
        IsBusy = false;
    }

    public async Task UpdateUser(string password)
    {
        IsBusy = true;
        User.PkUser = LoginData.UserLogin!.Id;
        User.Password = password;
        User.LastModificationDate = DateTime.Now;

        await _http.PostAsJsonAsync("api/security/UpdateUser", User);

        await AddLog("Ha actualizado las credenciales");
        IsBusy = false;
        Logout();
    }
}

public static class LoginData
{
    public static UserModel? UserLogin { get; set; }
}
