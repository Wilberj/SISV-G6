namespace QualityManagementApp.Server.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SurveyController : ControllerBase
{
    readonly List<string> notMapped = new() { "SurveyCategory", "Questions" };
    public SurveyController()
    {
        if (Auth.VerifyConnection() == false)
        {
            Auth.StartConnection();
        }
    }

    [HttpGet]
    public ActionResult GetSurveyCategories()
    {
        SurveyCategory category = new();
        return Ok(category.Get<SurveyCategory>());
    }

    [HttpGet("{surveyId}")]
    public ActionResult GetSurvey(string surveyId)
    {
        Survey survey = new();
        Question question = new();

        survey = survey.Get<Survey>("PkSurvey = '" + surveyId + "'", notMapped).FirstOrDefault()!;
        survey.Questions = question.Get<Question>("FkSurvey = '" + surveyId + "'");
        return Ok(survey);
    }

    [HttpGet("{surveyId}")]
    public ActionResult GetSurveyToInterviewed(string surveyId)
    {
        SurveyToInterviewed _survey = new();
        QuestionToInterviewed _question = new();
        AnswerToInterviewed _answer = new();

        List<object> _param = new()
        {
            surveyId
        };

        _survey = _survey.GetProcedure<SurveyToInterviewed>("GetSurveyToInterviewed", _param).FirstOrDefault()!;
        _survey.Questions = _question.GetProcedure<QuestionToInterviewed>("GetQuestionsToInterviewedToUser", _param);

        foreach (var question in _survey.Questions)
        {
            List<object> param = new() { question.Type, question.Id };

            var respuestas = _answer.GetProcedure<AnswerToInterviewed>("GetAnswerToInterviewed", param);

            _survey.Questions.Where(q => q.Id == question.Id).First().Answers = new();
            foreach (var item in respuestas)
            {
                _survey.Questions.Where(q => q.Id == question.Id).First().Answers!.Add(item);
            }
        }

        return Ok(_survey);
    }

    [HttpGet("{surveyId}")]
    public ActionResult GetSurveyToUser(string surveyId)
    {
        SurveyToUser _survey = new();
        QuestionToUser _question = new();
        ChartToUser _chart = new();

        List<object> _param = new()
        {
            surveyId
        };

        _survey = _survey.GetProcedure<SurveyToUser>("GetSurveyToUser", _param).FirstOrDefault()!;
        _survey.Questions = _question.GetProcedure<QuestionToUser>("GetQuestionsToInterviewedToUser", _param);

        foreach (var question in _survey.Questions)
        {
            List<object> param = new() { surveyId, question.Id };

            var respuestas = _chart.GetProcedure<ChartToUser>("GetQuestionInsightsToUser", param);

            _survey.Questions.Where(q => q.Id == question.Id).First().Charts = new();
            foreach (var item in respuestas)
            {
                _survey.Questions.Where(q => q.Id == question.Id).First().Charts!.Add(item);
            }
        }

        return Ok(_survey);
    }

    [HttpGet("{surveyId}")]
    public ActionResult GetAllObservations(string surveyId)
    {
        Observation observation = new();
        return Ok(observation.GetProcedure<Observation>("GetAllObservationsToUser", new() { surveyId }));
    }

    [HttpGet]
    public ActionResult GetSurveys()
    {
        Survey surveyInstance = new ();
        SurveyCategory categoryInstance = new();

        var surveys = surveyInstance.Get<Survey>(null, notMapped);

        foreach (var survey in surveys)
        {
            survey.SurveyCategory = categoryInstance.Get<SurveyCategory>($"PkSurveyCategory = {survey.FkSurveyCategory}").FirstOrDefault();
        }
        return Ok(surveys);
    }

    [HttpPost]
    public ActionResult PostSurvey(Survey survey)
    {
        try
        {
            survey.Save(notMapped);
            foreach (var question in survey.Questions!)
            {
                question.Save();
            }
            return GetSurvey(survey.PkSurvey);
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpGet]
    public ActionResult GetTypesQA()
    {
        TypeQA type = new();
        return Ok(type.Get<TypeQA>());
    }

    [HttpGet("{typeQAId}")]
    public ActionResult GetAnswersByTypeQA(int typeQAId)
    {
        Answer answer = new();
        return Ok(answer.Get<Answer>("FkTypeQA = '" + typeQAId + "'"));
    }

    [HttpPost]
    public ActionResult<string> PostInterviewed(Interviewed interviewed)
    {
        try
        {
            return Ok(interviewed.Save().ToString());
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public ActionResult PostSelectedAnswers(SelectedAnswer selectedAnswer)
    {
        try
        {
            return Ok(selectedAnswer.Save());
        }
        catch (Exception)
        {

            throw;
        }
    }
}
