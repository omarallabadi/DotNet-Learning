using StockApp_V4.Models;
using StockApp_V4.Services;
using StockApp_V4.ServicesContracts;

namespace StockApp_V4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
            builder.Services.AddScoped<IFinnhubService, MyService>();
            builder.Services.AddSingleton<IStocksService, StockServices>();

            var app = builder.Build();

            
            app.UseRouting();
            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
