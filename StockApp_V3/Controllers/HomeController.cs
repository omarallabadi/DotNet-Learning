using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockApp_V3.DTO;
using StockApp_V3.Models;
using StockApp_V3.ServicesContracts;

namespace StockApp_V3.Controllers
{
    [Route("trade")]

    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IFinnhubService _myService;
        private readonly IStocksService _stocksService;
        private readonly TradingOptions _options;

        public HomeController(IConfiguration configuration, IFinnhubService service, IStocksService stocksService, IOptions<TradingOptions> options)
        {
            _configuration = configuration;
            _myService = service;
            _stocksService = stocksService;
            _options = options.Value;
        }
        [Route("/")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            string sympol = _options.DefaultStockSymbol;
            string token = _configuration.GetValue<string>("token");
            Dictionary<string, object> response1 = await _myService.getStockPriceQuote(sympol, token);
            Dictionary<string, object> response2 = await _myService.GetCompanyProfile(sympol, token);
            StockTrade stock = new StockTrade()
            {
                StockSymbol = Convert.ToString(sympol),
                StockName = Convert.ToString(response2["name"]),
                Price = Convert.ToDouble(Convert.ToString(response1["c"])),
                Quantity = 1
            };



            return View(stock);
        }


        [Route("orders")]
        public async Task<IActionResult> Orders()
        {

            List<BuyOrderResponse> buyOrderResponses =await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();
            OrdersViewModel orders = new OrdersViewModel() { BuyOrders = buyOrderResponses, SellOrders = sellOrderResponses };
            return View(orders);
        }

        [Route("BuyOrder")]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(buyOrderRequest);
            if (!ModelState.IsValid)
            {

                ViewBag.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = buyOrderRequest.StockName, StockSymbol = buyOrderRequest    .StockSymbol , Price = buyOrderRequest.Price };
                return View("index",stockTrade);

            }
            
            await _stocksService.CreateBuyOrder(buyOrderRequest);
        



            return RedirectToAction(nameof(Orders));
        }

        [Route("SellOrder")]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
        {
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(sellOrderRequest);
            if (!ModelState.IsValid)
            {

                ViewBag.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = sellOrderRequest.StockName,  StockSymbol = sellOrderRequest.StockSymbol , Price = sellOrderRequest.Price };
                return View("index", stockTrade);

            }
           

            await _stocksService.CreateSellOrder(sellOrderRequest);
      




            return RedirectToAction(nameof(Orders));
        }
    }
}
