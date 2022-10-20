using QualityManagementApp.Client.Pages.Surveys;
using QualityManagementApp.Shared;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using static QualityManagementApp.Client.Pages.Surveys.SurveyInsights;
using static QualityManagementApp.Shared.Model;
using static QualityManagementApp.Shared.SurveyInsightModel;
using static System.Net.WebRequestMethods;

namespace QualityManagementApp.Client.Services
{
    public class SurveyInsightsService : ISurveyInsightsService
    {
        private readonly HttpClient _http;

        public SurveyInsightsService(HttpClient http)
        {
            _http = http;
        }

        public bool IsBusy { get; set; } = false;
        //public Model.Survey Survey { get; set; } = null!;
        public List<Model.Question> Questions { get; set; } = new();
        public List<SurveyInsightModel> Insight { get; set; } = null!;

        public async Task GetQuestions(string? surveyId)
        {
            var questions = await _http.GetFromJsonAsync<List<Question>>($"api/survey/GetQuestions/{surveyId}");
            Questions = questions ?? null!;
        }

        public List<Chart> ReturnInsight(string survey, int? question)
        {
            var value = new ToDo(survey, question.ToString());
            var task = Task.Run(async () => await _http.PostAsJsonAsync($"api/surveyinsight/GetCharts", value));         
            var i = task.Result;
            var j = i.Content.ReadFromJsonAsync<List<Chart>>();
            return j.Result?? null!;
        }
    }
}
