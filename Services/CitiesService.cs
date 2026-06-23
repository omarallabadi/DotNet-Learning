using ServiceContracts;
using WeatherApp_V5.Models;
namespace Services
{
    public class CitiesService: IWeatherService
    {
        private List<CityWeather> _cities;

       public CitiesService() { 
        _cities = new List<CityWeather>()
        {
                        new CityWeather(){ CityUniqueCode = "LDN", CityName = "London", DateAndTime =  DateTime.Parse( "2030-01-01 8:00"),  TemperatureFahrenheit = 33 },
new CityWeather(){ CityUniqueCode = "NYC", CityName = "New York", DateAndTime =  DateTime.Parse( "2030-01-01 3:00") ,  TemperatureFahrenheit = 60 },
        new CityWeather(){ CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 9:00") ,  TemperatureFahrenheit = 82 }


        };
        }

        public CityWeather? GetWeatherByCityCode(string CityCode)
        {
            CityWeather? city = _cities.FirstOrDefault(x => x.CityUniqueCode == CityCode);
            return city;
        }

        public List<CityWeather> GetWeatherDetails()
        {
            return _cities;
        }
    }
}
