using StockApp_V5.Models;
using StockApp_V5.Services;
using StockApp_V5.ServicesContracts;
using Microsoft.EntityFrameworkCore;

namespace StockApp_V5
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
            builder.Services.AddScoped<IStocksService, StockServices>();
            builder.Services.AddDbContext<StockMarketDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            Rotativa.AspNetCore.RotativaConfiguration.Setup(app.Environment.WebRootPath, wkhtmltopdfRelativePath: "Rotativa");

            app.UseRouting();
            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
