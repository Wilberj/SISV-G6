using Microsoft.AspNetCore.Components;
using static QualityManagementApp.Shared.Model;
using System.Net.Http.Json;
using QualityManagementApp.Client.Pages.Surveys;
using MudBlazor;

namespace QualityManagementApp.Client.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigation;

        public LoginService(HttpClient http, NavigationManager navigation)
        {
            _http = http;
            _navigation = navigation;
        }

        public bool IsBusy { get; set; }
        public string? Message { get; set; } = null;
        public UserDetail User { get; set; } = new();

        public async Task Login()
        {
            IsBusy = true;
            Message = null;
            var responseUser = await _http.PostAsJsonAsync("api/login/Login", User);

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
            LoginData.UserLogin = null;
            _navigation.NavigateTo("/");
        }
    }

    public static class LoginData
    {
        public static UserModel? UserLogin { get; set; }
    }
}
