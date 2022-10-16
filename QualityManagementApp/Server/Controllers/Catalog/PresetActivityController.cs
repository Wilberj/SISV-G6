using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Server.Controllers.Catalog
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PresetActivityController : ControllerBase
    {
        public PresetActivityController()
        {
            Auth.StartConnection();
        }

        [HttpGet]
        public ActionResult GetPresetActivities()
        {
            PresetActivity presetActivity = new();
            var ihsd = presetActivity.Get<PresetActivity>();
            return Ok(ihsd);
        }

        [HttpGet("{presetActivityId}", Name = "GetPresetActivity")]
        public ActionResult GetPresetActivity(string presetActivityId)
        {
            PresetActivity presetActivity = new();
            return Ok(presetActivity.Get<PresetActivity>("PkPresetActivity = '" + presetActivityId + "'").FirstOrDefault());
        }

        [HttpPost]
        public ActionResult PostPresetActivity(PresetActivity presetActivity)
        {
            try
            {
                presetActivity.Save();
                return Ok(new CreatedAtRouteResult("GetPresetActivity", new { presetActivityId = presetActivity.PkPresetActivity }, presetActivity));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
