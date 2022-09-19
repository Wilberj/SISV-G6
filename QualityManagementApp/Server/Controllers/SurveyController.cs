using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        public SurveyController()
        {
            Auth.LoginIn("tcalidad", "tcalidad");
        }

        [HttpGet]
        public ActionResult GetSurveyCategories()
        {
            SurveyCategory category = new();
            return Ok(category.Get<SurveyCategory>());
        }

        [HttpGet("{surveyId}", Name = "GetSurvey")]
        public ActionResult GetSurvey(string surveyId)
        {
            Survey survey = new();
            return Ok(survey.Get<Survey>("PkSurvey = '" + surveyId + "'").FirstOrDefault());
        }

        [HttpGet]
        public ActionResult GetSurveys()
        {
            Survey survey = new();
            return Ok(survey.Get<Survey>());
        }

        [HttpPost]
        public ActionResult PostSurvey(Survey survey)
        {
            try
            {
                survey.Save();
                return Ok(new CreatedAtRouteResult("GetSurvey", new { surveyId = survey.PkSurvey }, survey));
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

        [HttpGet("{surveyId}")]
        public ActionResult GetQuestions(string surveyId)
        {
            Question question = new();
            return Ok(question.Get<Question>("FkSurvey = '" + surveyId + "'"));
        }

        [HttpPost]
        public ActionResult PostQuestion(List<Model.Question> questions)
        {
            try
            {
                foreach (var question in questions)
                {
                    question.Save();
                }
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
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
        public ActionResult PostSelectedAnswers(List<SelectedAnswer> selectedAnswer)
        {
            try
            {
                foreach (var selected in selectedAnswer)
                {
                    selected.Save();
                }
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
