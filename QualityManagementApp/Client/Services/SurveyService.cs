namespace QualityManagementApp.Client.Services;

public class SurveyService : ISurveyService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigation;
    private readonly ISnackbar _snackbar;
    private readonly ISecurityService _security;

    public SurveyService(HttpClient http, NavigationManager navigation, ISnackbar snackbar, ISecurityService security)
    {
        _http = http;
        _navigation = navigation;
        _snackbar = snackbar;
        _security = security;
    }

    public bool IsBusy { get; set; } = false;
    public Survey Survey { get; set; } = new();
    public SurveyToInterviewed SurveyToInterviewed { get; set; } = new();
    public SurveyToUser SurveyToUser { get; set; } = new();
    public List<Survey>? Surveys { get; set; } = null;
    public SurveyCategory[] SurveyCategories { get; set; } = Array.Empty<SurveyCategory>();
    public TypeQA[] TypesQA { get; set; } = Array.Empty<TypeQA>();
    public Question Question { get; set; } = new();
    public Answer[]? AnswersByTypeQA { get; set; } = null;
    public Interviewed Interviewed { get; set; } = new();
    public List<SelectedAnswer> SelectedAnswers { get; set; } = new List<SelectedAnswer>();
    public List<Observation>? Observations { get; set; } = null;

    public void AddSelectedAnswer(int? questionId, int? answerId)
    {
        if (SelectedAnswers.Exists(a => a.FkQuestion == questionId))
        {
            var index = SelectedAnswers.FindIndex(i => i.FkQuestion == questionId);
            SelectedAnswers[index].FkAnswer = answerId;
        }
        else
        {
            var answer = new SelectedAnswer
            {
                FkQuestion = questionId,
                FkAnswer = answerId
            };
            SelectedAnswers.Add(answer);
        }
    }

    public async Task AddSelectedAnswersInterviewed()
    {
        IsBusy = true;

        Interviewed.Observation ??= "";
        Interviewed.CreationDate = DateTime.Now;
        var response = await _http.PostAsJsonAsync("api/survey/PostInterviewed", Interviewed);
        int id = Convert.ToInt32(await response.Content.ReadAsStringAsync());

        var ok = 0;
        foreach (var SelectedAnswer in SelectedAnswers)
        {
            SelectedAnswer.FkInterviewed = id;
            await _http.PostAsJsonAsync("api/survey/PostSelectedAnswers", SelectedAnswer);
            ok++;
        }

        if (ok > 0)
        {
            IsBusy = false;
            _navigation.NavigateTo("survey_added_response/" + "Gracias por contestar la encuesta", false, true);
        }
    }

    public async Task GetAnswersByTypeQA(int? typeQAId)
    {
        Question.FkTypeQA = typeQAId;
        AnswersByTypeQA = await _http.GetFromJsonAsync<Answer[]?>($"api/survey/GetAnswersByTypeQA/{typeQAId}");
    }

    public void AddQuestion()
    {
        Question.CreationDate = DateTime.Now;
        Question.FkSurvey = Survey.PkSurvey;
        Survey.Questions!.Add(Question);
        AnswersByTypeQA = null;
        Question = new();
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
        if (Survey.Questions!.Capacity != 0)
        {
            IsBusy = true;
            Survey.Description ??= "";
            Survey.CreationDate = DateTime.Now;
            Survey.LastModificationDate = DateTime.Now;

            var response = await _http.PostAsJsonAsync("api/survey/PostSurvey", Survey);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                IsBusy = false;
                var survey = await response.Content.ReadFromJsonAsync<Survey>();
                await _security.AddLog($"Ha añadido la encuesta {survey!.Title}");

                _snackbar.Add(survey!.Title + " Añadida.", Severity.Success, config => { config.HideIcon = true; });
                _navigation.NavigateTo("surveys");
            }

            IsBusy = false;
        }
    }

    public async Task GetSurvey(string surveyId, bool? isTest)
    {
        IsBusy = true;
        if (isTest == true)
        {
            var survey = await _http.GetFromJsonAsync<SurveyToInterviewed>($"api/survey/GetSurveyToInterviewed/{surveyId}");
            SurveyToInterviewed = survey!;
        }
        else
        {
            var survey = await _http.GetFromJsonAsync<Survey>($"api/survey/GetSurvey/{surveyId}");
            Survey = survey ?? null!;
        }
        IsBusy = false;
    }

    public async Task GetSurveyInsights(string surveyId)
    {
        IsBusy = true;
        var survey = await _http.GetFromJsonAsync<SurveyToUser>($"api/survey/GetSurveyToUser/{surveyId}");
        SurveyToUser = survey!;
        IsBusy = false;
    }

    public async Task GetSurveys()
    {
        IsBusy = true;
        Surveys = await _http.GetFromJsonAsync<List<Survey>>("api/survey/GetSurveys");
        IsBusy = false;
    }

    public async Task GetSurveyCategories()
    {
        IsBusy = true;
        SurveyCategory[]? surveyCategories = await _http.GetFromJsonAsync<SurveyCategory[]?>("api/survey/GetSurveyCategories");
        SurveyCategories = surveyCategories ?? Array.Empty<SurveyCategory>();
        IsBusy = false;
    }

    public void GenerateCode(string callbackString)
    {
        try
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
        catch (Exception)
        {
            _snackbar.Add("EL título tiene errores de escritura, por favor Corrijalos.", Severity.Error, config => { config.HideIcon = true; });
        }
    }

    public async Task GetAllObservations(string surveyId)
    {
        IsBusy = true;
        Observations = await _http.GetFromJsonAsync<List<Observation>?>($"api/survey/GetAllObservations/{surveyId}");
        IsBusy = false;
    }
}
