using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockApp_V6.Models;
using StockApp_V6.ServicesContracts;
using System.Text.Json;
using System.Web.Razor.Tokenizer.Symbols;


namespace StockApp_V6.Controllers
{
    [Route("[controller]")]

    public class StocksController : Controller
    {

        private readonly IFinnhubService _myService;
        private readonly IConfiguration _configuration;
        private readonly TradingOptions _options;
        private readonly ILogger<StocksController> _logger;

        public StocksController(IFinnhubService myService, IOptions<TradingOptions> options)
        {
            _myService = myService;
            _options = options.Value;
        }




        [Route("[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock)
        {


        

            List<Dictionary<string, string>>? stocks = await _myService.GetStocks();

            ViewBag.Stock = stock;                                                           

            if (stocks == null || stocks.Count == 0)
            {
                ViewBag.Message = "No stocks found.";
                return View(new List<Stock>());
            }

            string[] top25 = _options.Top25PopularStocks.Split(',');

            List<Stock> stockList = stocks
                .Select(s => new Stock
                {
                    StockSymbol = s.ContainsKey("symbol") ? s["symbol"] : null,
                    StockName = s.ContainsKey("description") ? s["description"] : null
                })
                .Where(s => top25.Contains(s.StockSymbol))
                .ToList();
            ViewBag.stock = stock;

            return View(stockList);
        }
    }
    }

