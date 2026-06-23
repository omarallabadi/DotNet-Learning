using WeatherApp_V5.Models;

namespace ServiceContracts
{
    public interface IWeatherService
    {

        List<CityWeather> GetWeatherDetails();

        CityWeather? GetWeatherByCityCode(string CityCode);
    }
}