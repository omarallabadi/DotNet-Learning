namespace WeatherApp_V5;
    using ServiceContracts;
using Services;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
        builder.Services.AddTransient<IWeatherService, CitiesService>(); var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllers();
            app.UseRouting();
           

            app.Run();
        }
    }

