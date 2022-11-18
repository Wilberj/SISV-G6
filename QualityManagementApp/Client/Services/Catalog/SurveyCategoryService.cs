namespace QualityManagementApp.Client.Services.Catalog
{
    public class SurveyCategoryService : ISurveyCategory
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigation;
        private readonly ISnackbar _snackbar;
        private readonly ISecurityService _security;

        public SurveyCategoryService(HttpClient http, NavigationManager navigation, ISnackbar snackbar, ISecurityService security)
        {
            _http = http;
            _navigation = navigation;
            _snackbar = snackbar;
            _security = security;
        }

        public bool IsBusy { get; set; } = false;
        public SurveyCategory SurveyCategory { get; set; } = new();
        public List<SurveyCategory>? SurveyCategories { get; set; } = null;

        public async Task AddSurveyCategory()
        {
            IsBusy = true;

            var response = await _http.PostAsJsonAsync("api/surveycategory/PostSurveyCategory", SurveyCategory);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IsBusy = false;
                await GetSurveyCategories();

                var surveyCategory = await response.Content.ReadFromJsonAsync<PresetActivity>();
                await _security.AddLog($"Ha añadido la Categoria de encuesta {surveyCategory!.Title}");

                _snackbar.Add("La categoria de encuesta " + surveyCategory.Title + " fue Añadida.", Severity.Success, config => { config.HideIcon = true; });

                _navigation.NavigateTo("surveyCategory");
            }
            IsBusy = false;
        }

        public async Task GetSurveyCategory(int? surveyCategoryId)
        {
            IsBusy = true;
            var surveyCategory = await _http.GetFromJsonAsync<SurveyCategory>($"api/surveycategory/GetSurveyCategory/{surveyCategoryId}");
            SurveyCategory = surveyCategory ?? null!;
            IsBusy = false;
        }

        public async Task GetSurveyCategories()
        {
            IsBusy = true;
            SurveyCategories = await _http.GetFromJsonAsync<List<SurveyCategory>>("api/surveycategory/GetSurveyCategories");
            IsBusy = false;
        }

        public async Task DeleteSurveyCategory()
        {
            IsBusy = true;

            var response = await _http.PostAsJsonAsync("api/surveycategory/DeleteSurveyCategory", SurveyCategory);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IsBusy = false;
                await _security.AddLog($"Ha eliminado la categoria de encuesta {SurveyCategory.Title}");

                _snackbar.Add("La categoria de encuesta " + SurveyCategory.Title + " fue Eliminada.", Severity.Error, config => { config.HideIcon = true; });

                await GetSurveyCategories();
                _navigation.NavigateTo("surveyCategories");
            }
            IsBusy = false;

        }

        public async Task<bool> UpdateSurveyCategory()
        {
            IsBusy = true;

            var response = await _http.PostAsJsonAsync("api/surveycategory/UpdateSurveyCategory", SurveyCategory);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IsBusy = false;
                var surveyCategory = await response.Content.ReadFromJsonAsync<SurveyCategory>();
                SurveyCategory = surveyCategory!;

                await _security.AddLog($"Ha modificado la categoria de encuesta {surveyCategory!.Title}");
                _snackbar.Add("La categoria de encuesta " + surveyCategory.Title + " fue Actualizada.", Severity.Info, config => { config.HideIcon = true; });
            }
            IsBusy = false;

            return true;
        }
    }
}
