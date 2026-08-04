using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockApp_V1.Models;
using StockApp_V1.ServicesContracts;

namespace StockApp_V1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IFinnhubService _myService;
        private readonly TradingOptions _options;

        public HomeController(IConfiguration configuration,IFinnhubService service,IOptions<TradingOptions> options)
        {
            _configuration = configuration;
            _myService = service;
            _options=options.Value;
        }
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            string sympol = _options.DefaultStockSymbol;
            string token = _configuration.GetValue<string>("token");
              Dictionary<string,object> response1= await _myService.getStockPriceQuote(sympol, token);
            Dictionary<string, object> response2 = await _myService.GetCompanyProfile(sympol, token);
            StockTrade stock = new StockTrade()
            {
                StockSymbol = Convert.ToString( sympol),
                StockName = Convert.ToString( response2["name"]),
                Price = Convert.ToDouble(Convert.ToString( response1["c"])),
                Quantity =4
            };



            return View(stock);
        }
    }
}
