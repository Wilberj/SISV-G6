namespace QualityManagementApp.Server.Controllers.Catalog;

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
        return Ok(position.Get<Position>());
    }

    [HttpGet("{positionId}")]
    public ActionResult GetPosition(int positionId)
    {
        Position position = new();
        return Ok(position.Get<Position>("PkPosition = " + positionId).FirstOrDefault());
    }

    [HttpPost]
    public ActionResult PostPosition(Position position)
    {
        try
        {
            var id = position.Save();
            return GetPosition((int)id);
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public ActionResult DeletePosition(Position position)
    {
        try
        {
            return Ok(position.Delete());
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public ActionResult Updateposition(Position position)
    {
        try
        {
            position.Update("PkPosition");
            return GetPosition((int)position.PkPosition!);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
