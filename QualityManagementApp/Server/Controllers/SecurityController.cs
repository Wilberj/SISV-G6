namespace QualityManagementApp.Server.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SecurityController : ControllerBase
{
    public SecurityController()
    {
        Auth.StartConnection();
    }

    [HttpPost]
    public ActionResult<UserModel> Login(UserDetail user)
    {
        UserModel User = new(user.FindObject<UserDetail>() ?? new());
        return Ok(User ?? null);
    }

    [HttpPost]
    public ActionResult<UserModel> PostLog(UserLog log)
    {
        log.Save();
        return Ok();
    }

    [HttpGet("{id}")]
    public ActionResult<UserModel> GetLogs(int id)
    {
        UserLog logInstance = new();
        return Ok(logInstance.Get<UserLog>($"FkUser = {id} ORDER BY CreationDate DESC"));
    }

    [HttpPost]
    public ActionResult UpdateUser(UserDetail user)
    {
        user.Update("PkUser");
        return Ok();
    }
}
