using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Server.Controllers.Catalog
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        public PositionController()
        {
            Auth.StartConnection();
        }

        [HttpGet]
        public ActionResult GetPositions()
        {
            Position position = new();
            var ihsd = position.Get<Position>();
            return Ok(ihsd);
        }

        [HttpGet("{positionId}", Name = "GetPosition")]
        public ActionResult GetPosition(int positionId)
        {
            Position position = new();
            return Ok(position.Get<Position>("PkPosition = '" + positionId + "'").FirstOrDefault());
        }

        [HttpPost]
        public ActionResult PostPosition(Position position)
        {
            try
            {
                position.Save();
                return Ok(new CreatedAtRouteResult("GetPosition", new { positionId = position.PkPosition }, position));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
