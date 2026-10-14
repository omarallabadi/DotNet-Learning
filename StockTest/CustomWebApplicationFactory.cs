using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StockApp_V6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRUDTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);





            builder.UseEnvironment("Test");



            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddUserSecrets<Program>(optional: true);
            });

            builder.ConfigureServices(Services =>
            {
                Services.RemoveAll(typeof(DbContextOptions<StockMarketDbContext>));
                Services.RemoveAll(typeof(StockMarketDbContext));
                Services.RemoveAll(typeof(IDbContextOptionsConfiguration<StockMarketDbContext>));
                Services.AddDbContext<StockMarketDbContext>(options =>
                    options.UseInMemoryDatabase("DatabaseForTesting"));
            });
        }
    }
}
