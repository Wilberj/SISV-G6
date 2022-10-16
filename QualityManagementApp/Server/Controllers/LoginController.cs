using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualityManagementApp.Client.Pages.Surveys;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public LoginController()
        {
            Auth.StartConnection();
        }

        [HttpPost]
        public ActionResult<UserModel> Login(UserDetail user)
        {
            UserModel User = new(user.FindObject<UserDetail>()?? new());
            return Ok(User?? null);
        }
    }
}
