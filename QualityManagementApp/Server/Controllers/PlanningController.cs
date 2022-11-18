namespace QualityManagementApp.Server.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PlanningController : ControllerBase
{
    readonly List<string> notMapped = new() { "Status", "Activities", "Details", "Employee", "City", "Position" };

    [HttpGet]
    public ActionResult GetPlannings()
    {
        Planning planningInstance = new();
        Status statusInstance = new();

        var plannings = planningInstance.Get<Planning>(null, notMapped);

        foreach (var planning in plannings)
        {
            planning.Status = statusInstance.Get<Status>($"PkStatus = {planning.FkStatus}").FirstOrDefault();
        }

        return Ok(plannings);
    }

    [HttpGet("{planningId}")]
    public ActionResult GetPlanning(int planningId)
    {
        Planning planningInstance = new();
        Status statusInstance = new();
        Activity activityInstance = new();

        planningInstance = planningInstance.Get<Planning>($"PkPlanning = {planningId}", notMapped).FirstOrDefault()!;
        planningInstance.Status = statusInstance.Get<Status>($"PkStatus = {planningInstance.FkStatus}").FirstOrDefault();
        planningInstance.Activities = activityInstance.Get<Activity>($"FkPlanning = {planningId}", notMapped);

        return Ok(planningInstance);
    }

    [HttpGet("{activityId}")]
    public ActionResult GetActivity(int activityId)
    {
        Activity activityInstance = new();
        Status statusInstance = new();
        ActivityDetail detailInstance = new();
        Employee employeeInstance = new();

        activityInstance = activityInstance.Get<Activity>($"PkActivity = {activityId}", notMapped).FirstOrDefault()!;
        activityInstance.Status = statusInstance.Get<Status>($"PkStatus = {activityInstance.FkStatus}").FirstOrDefault();
        activityInstance.Details = detailInstance.Get<ActivityDetail>($"FkActivity = {activityId}", notMapped);

        foreach (var detail in activityInstance.Details)
        {
            detail.Employee = employeeInstance.Get<Employee>($"PkEmployee = {detail.FkEmployee}", notMapped).FirstOrDefault();
        }

        return Ok(activityInstance);
    }

    [HttpGet]
    public ActionResult GetStatuses()
    {
        Status statusInstance = new();

        return Ok(statusInstance.Get<Status>());
    }

    [HttpPost]
    public ActionResult PostPlanning(Planning planning)
    {
        try
        {
            int planningId = (int)planning.Save(notMapped);
            foreach (var activity in planning.Activities!)
            {
                activity.FkPlanning = planningId;
                int activityId = (int)activity.Save(notMapped);

                foreach (var detail in activity.Details!)
                {
                    detail.FkActivity = activityId;
                    detail.Save(notMapped);
                }
            }

            return GetPlanning(planningId);
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public ActionResult UpdatePlanningStatus(Planning planning)
    {
        planning.Update("PkPlanning");
        return Ok();
    }

    [HttpPost]
    public ActionResult UpdateActivityStatus(Activity activity)
    {
        activity.Update("PkActivity");
        return Ok();
    }

    [HttpPost]
    public ActionResult UpdateActivity(Activity activity)
    {
        Activity activityInstance = new();
        var predecessor = activityInstance.GetProcedure<Activity>("GetPredecessor", new() { activity.FkPlanning!, activity.PkActivity! });
        if (predecessor.Count == 0)
        {
            return NoContent();
        }
        else
        {
            List<object> _param = new() { activity.FkPlanning! };
            activity.Update("PkActivity");
            activity.GetProcedure<bool>("SetPlanningValues", _param);
            return Ok();
        }
    }
}
