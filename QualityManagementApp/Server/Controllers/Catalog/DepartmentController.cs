using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Server.Controllers.Catalog
{
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
            var ihsd = department.Get<Department>();
            return Ok(ihsd);
        }

        [HttpGet("{departmentId}", Name = "GetDepartment")]
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
                department.Save();
                return Ok(new CreatedAtRouteResult("GetDepartment", new { departmentId = department.PkDepartment }, department));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
