using Microsoft.EntityFrameworkCore;
using Serilog;
using StockApp_V6.Middleware;
using StockApp_V6.Models;
using StockApp_V6.Repositories;
using StockApp_V6.RepositoriesContracts;
using StockApp_V6.Services;
using StockApp_V6.ServicesContracts;

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
            builder.Host.UseSerilog((context, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services);
            });


            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Trade/Error");

                app.UseExceptionHandlingMiddleware();

            }
       
            








            app.UseSerilogRequestLogging();

            Rotativa.AspNetCore.RotativaConfiguration.Setup(app.Environment.WebRootPath, wkhtmltopdfRelativePath: "Rotativa");

            app.UseRouting();
            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
public partial class Program { }
