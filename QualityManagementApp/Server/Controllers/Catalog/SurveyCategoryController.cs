namespace QualityManagementApp.Server.Controllers.Catalog
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SurveyCategoryController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetSurveyCategories()
        {
            SurveyCategory surveyCategory = new();
            return Ok(surveyCategory.Get<SurveyCategory>());
        }

        [HttpGet("{surveyCategoryId}")]
        public ActionResult GetSurveyCategory(int surveyCategoryId)
        {
            SurveyCategory surveyCategory = new();
            return Ok(surveyCategory.Get<SurveyCategory>("PkSurveyCategory = " + surveyCategoryId).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult PostSurveyCategory(SurveyCategory surveyCategory)
        {
            try
            {
                var id = surveyCategory.Save();
                return GetSurveyCategory((int)id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult DeleteSurveyCategory(SurveyCategory surveyCategory)
        {
            try
            {
                return Ok(surveyCategory.Delete());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult UpdateSurveyCategory(SurveyCategory surveyCategory)
        {
            try
            {
                surveyCategory.Update("PkSurveyCategory");
                return GetSurveyCategory((int)surveyCategory.PkSurveyCategory!);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
