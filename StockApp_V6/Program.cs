using StockApp_V6.Models;
using StockApp_V6.Services;
using StockApp_V6.ServicesContracts;
using Microsoft.EntityFrameworkCore;
using StockApp_V6.RepositoriesContracts;
using StockApp_V6.Repositories;

namespace StockApp_V6
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
            builder.Services.AddScoped<IStocksRepository, StocksRepository>();
            builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();
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
public partial class Program { }
