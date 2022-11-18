namespace QualityManagementApp.Server.Controllers.Catalog;

[Route("api/[controller]/[action]")]
[ApiController]
public class CityController : ControllerBase
{
    readonly List<string> notMapped = new() { "Department" };
    public CityController()
    {
        Auth.StartConnection();
    }

    [HttpGet]
    public ActionResult GetCities()
    {
        City city = new();
        Department department = new();


        var cities = city.Get<City>(null, notMapped);
        foreach (var item in cities)
        {
            item.Department = department.Get<Department>($"PkDepartment = {item.FkDepartment}").FirstOrDefault();
        }
        return Ok(cities);
    }

    [HttpGet("{cityId}")]
    public ActionResult GetCity(int? cityId)
    {
        City city = new();
        return Ok(city.Get<City>("PkCity = '" + cityId + "'", notMapped).FirstOrDefault());
    }

    [HttpPost]
    public ActionResult PostCity(City city)
    {
        try
        {
            var id = city.Save();
            return GetCity((int)id);
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public ActionResult DeleteCity(City city)
    {
        try
        {
            return Ok(city.Delete());
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public ActionResult UpdateCity(City city)
    {
        try
        {
            city.Update("PkCity");
            return GetCity(city.PkCity);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
