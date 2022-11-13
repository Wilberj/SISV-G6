namespace QualityManagementApp.Server.Controllers.Catalog;

[Route("api/[controller]/[action]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    readonly List<string> notMapped = new() { "City", "Position" };

    [HttpGet("{employeeId}")]
    public ActionResult GetEmployee(int employeeId)
    {
        Employee employee = new();
        return Ok(employee.Get<Employee>("PkEmployee = " + employeeId, notMapped).FirstOrDefault());
    }

    [HttpPost]
    public ActionResult PostEmployee(Employee employee)
    {
        try
        {
            var id = employee.Save();
            return GetEmployee((int)id);
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpGet]
    public ActionResult GetEmployees()
    {
        Employee employee = new();
        City city = new();
        Position position = new();

        var employees = employee.Get<Employee>(null, notMapped);
        foreach (var item in employees)
        {
            item.City = city.Get<City>($"PkCity = {item.FkCity}", new() { "Department" }).FirstOrDefault();
            item.Position = position.Get<Position>($"PkPosition = {item.FkPosition}").FirstOrDefault();
        }

        return Ok(employees);
    }

    [HttpPost]
    public ActionResult DeleteEmployee(Employee employee)
    {
        try
        {
            return Ok(employee.Delete());
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public ActionResult UpdateEmployee(Employee employee)
    {
        try
        {
            employee.Update("PkEmployee");
            return GetEmployee((int)employee.PkEmployee!);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
