using Microsoft.AspNetCore.Components;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;
using System.Net.Http.Json;
using QualityManagementApp.Client.Services.Contracts.Catalog;

namespace QualityManagementApp.Client.Services.Catalog
{
    public class PositionService : IPositionService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigation;

        public PositionService(HttpClient http, NavigationManager navigation)
        {
            _http = http;
            _navigation = navigation;
        }

        public bool IsBusy { get; set; } = false;
        //public Snackbar Snackbar { get; set; } = new();
        public Position Position { get; set; } = new();
        public Position[]? Positions { get; set; } = null;

        //public void AddPosition()
        //{
        //    IsBusy = true;
        //    Question.CreationDate = DateTime.Now;
        //    Question.FkSurvey = Survey.PkSurvey;
        //    Questions.Add(Question);
        //    AnswersByTypeQA = null;
        //    Question = new();
        //    IsBusy = false;
        //}


        public async Task AddPosition()
        {
            IsBusy = true;

            var response = await _http.PostAsJsonAsync("api/position/PostPosition", Position);

            //Snackbar.SnackbarIsOpen = true;
            //Snackbar.Message = $"El cargo fue agregado con exito";

            IsBusy = false;
            _navigation.NavigateTo("positions");
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
            var jjh = await _http.GetFromJsonAsync<Position[]>("api/position/GetPositions");
            Positions = jjh;
        }
    }
}
