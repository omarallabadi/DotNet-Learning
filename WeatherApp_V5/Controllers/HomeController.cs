using Microsoft.AspNetCore.Mvc;
using WeatherApp_V5.Models;
using ServiceContracts;


namespace WeatherApp_V5.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService;
        public HomeController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [Route("/")]
        public IActionResult Index()
        {



            //invoke service method
            var cities = _weatherService.GetWeatherDetails();

            //send cities collection to "Views/Weather/Index" view
            return View(cities);
        }





       
        [Route("/contact")]
        public IActionResult Contact()
        {
    


            return View();

        }










        [Route("/weather/{cityCode}")]
        public IActionResult Details(string cityCode)
        {
            var city = _weatherService.GetWeatherByCityCode(cityCode);
            return View(city);



        }
    }
}
