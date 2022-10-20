using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualityManagementApp.Shared;
using static QualityManagementApp.Shared.Model;

namespace QualityManagementApp.Server.Controllers.Catalog
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        public CityController()
        {
            Auth.StartConnection();
        }

        [HttpGet]
        public ActionResult GetCities()
        {
            City city = new();
            var ihsd = city.Get<City>();
            return Ok(ihsd);
        }

        [HttpGet("{cityId}", Name = "GetCity")]
        public ActionResult GetCity(int cityId)
        {
            City city = new();
            return Ok(city.Get<City>("PkCity = '" + cityId + "'").FirstOrDefault());
        }

        [HttpPost]
        public ActionResult PostCity(City city)
        {
            try
            {
                city.Save();
                return Ok(new CreatedAtRouteResult("GetCity", new { cityId = city.PkCity }, city));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult GetDepartments()
        {
            Department department = new();
            return Ok(department.Get<Department>());
        }
    }
}
