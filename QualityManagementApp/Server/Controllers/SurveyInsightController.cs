using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualityManagementApp.Shared;
using System.Linq;
using static QualityManagementApp.Shared.Model;
using static QualityManagementApp.Shared.SurveyInsightModel;

namespace QualityManagementApp.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SurveyInsightController : ControllerBase
    {
        public SurveyInsightController()
        {
            if (Auth.VerifyConnection() == false)
            {
                Auth.StartConnection();
            }
        }

        //[HttpGet("{surveyId}")]
        //public ActionResult GetQuestions(string surveyId)
        //{
        //    Question question = new();
        //    question.Charts.GetProcedure
        //    return Ok(question.Get<Question>("FkSurvey = '" + surveyId + "'"));
        //}

        [HttpPost]
        public ActionResult<List<Chart>> GetCharts(ToDo value)
        {
            var survey = value.survey;
            var question = value.question;

            string[] param = new string[] { "@survey", "@question" };
            string[] values = new string[] { value.survey, value.question??"" };
            Chart chart = new();
            return Ok(/*chart.GetProcedure<Chart>("SelectQuestionInsights", param)*/);
        }
    }
}
