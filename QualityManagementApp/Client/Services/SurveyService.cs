﻿using MatBlazor;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using static QualityManagementApp.Shared.Model;
using Microsoft.AspNetCore.Components.Web;
using QualityManagementApp.Shared;
using System.Globalization;

namespace QualityManagementApp.Client.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigation;

        public SurveyService(HttpClient http, NavigationManager navigation)
        {
            _http = http;
            _navigation = navigation;
        }

        public bool IsBusy { get; set; } = false;
        public Snackbar Snackbar { get; set; } = new();
        public Survey Survey { get; set; } = new();
        public Survey[]? Surveys { get; set; } = null!;
        public SurveyCategory[] SurveyCategories { get; set; } = Array.Empty<SurveyCategory>();
        public TypeQA[] TypesQA { get; set; } = Array.Empty<TypeQA>();
        public Question Question { get; set; } = new();
        public List<Question> Questions { get; set; } = new List<Question>();

        public Answer[]? AnswersByTypeQA { get; set; } = null;

        public Interviewed Interviewed { get; set; } = null!;

        public List<SelectedAnswer> SelectedAnswers { get; set; } = new List<SelectedAnswer>();

        public void AddSelectedAnswer(int? questionId, int? answerId)
        {
            var answer = new SelectedAnswer
            {
                FkInterviewed = Interviewed.PkInterviewed,
                FkQuestion = questionId,
                PkSelectedAnswer = answerId
            };
            SelectedAnswers.Add(answer);
        }

        public async Task AddSelectedAnswers()
        {
            IsBusy = true;

            var response = await _http.PostAsJsonAsync("api/survey/PostSelectedAnswers", SelectedAnswers);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Interviewed = new();
                Survey = new();
                Questions = new();
                SelectedAnswers = new();
                AnswersByTypeQA = null;
            }

            IsBusy = false;
        }

        public async Task<int> AddInterviewed()
        {
            IsBusy = true;
            Interviewed.CreationDate = DateTime.Now;
            var response = await _http.PostAsJsonAsync("api/survey/PostInterviewed", Interviewed);
            var result = await response.Content.ReadAsStringAsync();
            IsBusy = false;
            return Convert.ToInt32(result);
        }

        public async Task<Answer[]> ReturnAnswersByTypeQA(int? typeQAId)
        {
            var result = await _http.GetFromJsonAsync<Answer[]?>($"api/survey/GetAnswersByTypeQA/{typeQAId}");
            return result ?? null!;
        }

        public async Task GetAnswersByTypeQA(int? typeQAId)
        {
            IsBusy = true;
            Question.FkTypeQA = typeQAId != null ? typeQAId : null;
            AnswersByTypeQA = await _http.GetFromJsonAsync<Answer[]?>($"api/survey/GetAnswersByTypeQA/{typeQAId}");
            IsBusy = false;
        }

        public void AddQuestion()
        {
            IsBusy = true;
            Question.CreationDate = DateTime.Now;
            Question.FkSurvey = Survey.PkSurvey;
            Questions.Add(Question);
            AnswersByTypeQA = null;
            Question = new();
            IsBusy = false;
        }

        public async Task GetTypesQA()
        {
            IsBusy = true;
            TypeQA[]? typesQA = await _http.GetFromJsonAsync<TypeQA[]?>("api/survey/GetTypesQA");
            TypesQA = typesQA ?? Array.Empty<TypeQA>();
            IsBusy = false;
        }

        public async Task AddSurvey()
        {
            if (Questions.Capacity != 0)
            {
                IsBusy = true;
                Survey.CreationDate = DateTime.Now;
                Survey.LastModificationDate = DateTime.Now;

                var response = await _http.PostAsJsonAsync("api/survey/PostSurvey", Survey);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response = await _http.PostAsJsonAsync("api/survey/PostQuestion", Questions);
                }
                else
                {
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                }

                Snackbar.SnackbarIsOpen = true;
                Snackbar.Message = $"La encuesta fue agregada con exito";

                IsBusy = false;
                _navigation.NavigateTo("surveys");
            }
        }

        public async Task GetSurvey(string surveyId)
        {
            IsBusy = true;
            var survey = await _http.GetFromJsonAsync<Survey>($"api/survey/GetSurvey/{surveyId}");
            var questions = await _http.GetFromJsonAsync<List<Question>>($"api/survey/GetQuestions/{surveyId}");
            Survey = survey ?? null!;
            Questions = questions ?? null!;
            IsBusy = false;
        }

        public async Task GetSurveys()
        {
            Surveys = await _http.GetFromJsonAsync<Survey[]>("api/survey/GetSurveys");
        }

        public async Task GetSurveyCategories()
        {
            IsBusy = true;
            SurveyCategory[]? surveyCategories = await _http.GetFromJsonAsync<SurveyCategory[]?>("api/survey/GetSurveyCategories");
            SurveyCategories = surveyCategories ?? Array.Empty<SurveyCategory>();
            IsBusy = false;
        }

        public void GenerateCode(FocusEventArgs focusEvent)
        {
            if (Survey.Title != "" && Survey.Title != null)
            {
                var code = "";
                foreach (string schar in from c in Survey.Title.Split(' ') select c[..1])
                {
                    code += schar;
                }

                Survey.PkSurvey = code + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year;
            }
        }
    }
}
