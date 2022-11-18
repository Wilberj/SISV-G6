namespace QualityManagementApp.Server.Controllers.Catalog;

[Route("api/[controller]/[action]")]
[ApiController]
public class PresetActivityController : ControllerBase
{
    [HttpGet]
    public ActionResult GetPresetActivities()
    {
        PresetActivity presetActivity = new();
        return Ok(presetActivity.Get<PresetActivity>());
    }

    [HttpGet("{presetActivityId}")]
    public ActionResult GetPresetActivity(int presetActivityId)
    {
        PresetActivity presetActivity = new();
        return Ok(presetActivity.Get<PresetActivity>("PkPresetActivity = " + presetActivityId).FirstOrDefault());
    }

    [HttpPost]
    public ActionResult PostPresetActivity(PresetActivity presetActivity)
    {
        try
        {
            var id = presetActivity.Save();
            return GetPresetActivity((int)id);
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public ActionResult DeletePresetActivity(PresetActivity preset)
    {
        try
        {
            return Ok(preset.Delete());
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public ActionResult UpdatePresetActivity(PresetActivity preset)
    {
        try
        {
            preset.Update("PkPresetActivity");
            return GetPresetActivity((int)preset.PkPresetActivity!);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
