using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Server.Controllers.Catalog
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController()
        {
            Auth.StartConnection();
        }

        [HttpGet("{employeeId}", Name = "GetEmployee")]
        public ActionResult GetEmployee(string employeeId)
        {
            Employee employee = new();
            return Ok(employee.Get<Employee>("PkEmployee = '" + employeeId + "'").FirstOrDefault());
        }

        [HttpPost]
        public ActionResult PostEmployee(Employee employee)
        {
            try
            {
                employee.Save();
                return Ok(new CreatedAtRouteResult("GetEmployee", new { employeeId = employee.PkEmployee }, employee));
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
            return Ok(employee.Get<Employee>());
        }

        //[HttpGet]
        //public ActionResult GetCities()
        //{
        //    City city = new();
        //    return Ok(city.Get<City>());
        //}

        [HttpGet]
        public ActionResult GetPositions()
        {
            Position position = new();
            return Ok(position.Get<Position>());
        }

        [HttpGet]
        public ActionResult GetCity()
        {
            City city = new();
            return Ok(city.Get<City>());
        }
    }
}
