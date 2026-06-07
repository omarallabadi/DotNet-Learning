using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {


        [Route("/")]
        public IActionResult Index()
        {
            List<CityWeather> list = new List<CityWeather>()
            {
                new CityWeather(){ CityUniqueCode = "LDN", CityName = "London", DateAndTime =  DateTime.Parse( "2030-01-01 8:00"),  TemperatureFahrenheit = 33 },
new CityWeather(){ CityUniqueCode = "NYC", CityName = "New York", DateAndTime =  DateTime.Parse( "2030-01-01 3:00") ,  TemperatureFahrenheit = 60 },
        new CityWeather(){ CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 9:00") ,  TemperatureFahrenheit = 82 }
            };




            return View(list);

        }

        [Route("/weather/{cityCode}")]
        public IActionResult Details(string cityCode)
        {
            List<CityWeather> list = new List<CityWeather>()
            {
                new CityWeather(){ CityUniqueCode = "LDN", CityName = "London", DateAndTime =  DateTime.Parse( "2030-01-01 8:00"),  TemperatureFahrenheit = 33 },
new CityWeather(){ CityUniqueCode = "NYC", CityName = "New York", DateAndTime =  DateTime.Parse( "2030-01-01 3:00") ,  TemperatureFahrenheit = 60 },
        new CityWeather(){ CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 9:00") ,  TemperatureFahrenheit = 82 }
        
            };
            CityWeather? match = list
        .FirstOrDefault(city => city.CityUniqueCode == cityCode);

            if (match == null) {
                Response.StatusCode = 404;
                return View("Error",cityCode);
            }
          

            return View(match);



        }
    }
}
