using Microsoft.AspNetCore.Mvc;
using WeatherApp_V4.Models;

namespace WeatherApp_V4.ViewComponents
{
    public class GridViewComponent:ViewComponent
    {

public async Task<IViewComponentResult> InvokeAsync(CityWeather City)
        {
            ViewBag.temp = getcolor((City.TemperatureFahrenheit??0));
      



            return View(City);
        }


        public string getcolor(int temp)
        {

            string bgColor;

            if (temp < 44)
            {
                bgColor = "blue-back";
            }
            else if (temp >= 44 && temp <= 74)
            {
                bgColor = "green-back";
            }
            else
            {
                bgColor = "orange-back";
            }
            return bgColor;
        }

    }
}
