using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIOne.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
       
        public List<Weather> GetWeather()
        {
            List<Weather> weatherList = new List<Weather>();

            weatherList.Add(new Weather { CityName = "Delhi", Temperature = Random.Shared.Next(-20, 55) });
            weatherList.Add(new Weather { CityName = "Shimla", Temperature = Random.Shared.Next(-20, 55) });
            weatherList.Add(new Weather { CityName = "Hyderabad", Temperature = Random.Shared.Next(-20, 55) });
            weatherList.Add(new Weather { CityName = "Mumbai", Temperature = Random.Shared.Next(-20, 55) });
            weatherList.Add(new Weather { CityName = "Chennai", Temperature = Random.Shared.Next(-20, 55) });

            return weatherList;
        }


        [HttpGet]
        [Authorize]
        public IEnumerable<Employee> GetEmployees()
        {
            List<Employee> employeeList = new List<Employee>();

            employeeList.Add(new Employee { Id = 5, Name = "Raksha", Address = "Hyderabad" });
            employeeList.Add(new Employee { Id = 6, Name = "Madhu", Address = "Chennai" });
            employeeList.Add(new Employee { Id = 9, Name = "Rishi", Address = "Banglore" });
            employeeList.Add(new Employee { Id = 3, Name = "Manjusha", Address = "Hyderabad" });

            return employeeList;

        }
        [HttpGet]
        [AllowAnonymous]

        public ActionResult Greet()
        {
            return Ok("Hiii!!");
        }

    }
}