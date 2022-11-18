namespace QualityManagementApp.Server.Controllers.Catalog;

[Route("api/[controller]/[action]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    public DepartmentController()
    {
        Auth.StartConnection();
    }

    [HttpGet]
    public ActionResult GetDepartments()
    {
        Department department = new();
        return Ok(department.Get<Department>());
    }

    [HttpGet("{departmentId}")]
    public ActionResult GetDepartment(int departmentId)
    {
        Department department = new();
        return Ok(department.Get<Department>("PkDepartment = '" + departmentId + "'").FirstOrDefault());
    }

    [HttpPost]
    public ActionResult PostDepartment(Department department)
    {
        try
        {
            var id =department.Save();
            return GetDepartment((int)id);
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public ActionResult DeleteDepartment(Department department)
    {
        try
        {
            return Ok(department.Delete());
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public ActionResult UpdateDepartment(Department department)
    {
        try
        {
            department.Update("PkDepartment");
            return GetDepartment((int)department.PkDepartment!);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
