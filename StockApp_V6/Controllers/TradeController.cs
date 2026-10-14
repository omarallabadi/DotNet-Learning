using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using StockApp_V6.DTO;
using StockApp_V6.Models;
using StockApp_V6.ServicesContracts;



namespace StockApp_V6.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        
        private readonly IFinnhubService _myService;
        private readonly IStocksService _stocksService;
        private readonly TradingOptions _options;

        public TradeController( IFinnhubService service, IStocksService stocksService, IOptions<TradingOptions> options)
        {
            
            _myService = service;
            _stocksService = stocksService;
            _options = options.Value;
        }
        [Route("/")]
        [Route("[action]")]
        public async Task<IActionResult> Index(string? stock)
        {
            string sympol = stock ?? "MSFT";
            Dictionary<string, object> response1 = await _myService.getStockPriceQuote(sympol);
            Dictionary<string, object> response2 = await _myService.GetCompanyProfile(sympol);
            StockTrade resultStock = new StockTrade()
            {
                StockSymbol = Convert.ToString(sympol),
                StockName = Convert.ToString(response2["name"]),
                Price = Convert.ToDouble(Convert.ToString(response1["c"])),
                Quantity = _options.DefaultOrderQuantity
            };



            return View(resultStock);
        }


        [Route("[action]")]
        public async Task<IActionResult> Orders()
        {

            List<BuyOrderResponse> buyOrderResponses =await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();
            OrdersViewModel orders = new OrdersViewModel() { BuyOrders = buyOrderResponses, SellOrders = sellOrderResponses };
            return View(orders);
        }

        [Route("[action]")]
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

        [Route("[action]")]
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
        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {

            OrdersViewModel orders = new OrdersViewModel()
            {
                BuyOrders = await _stocksService.GetBuyOrders(),
                SellOrders = await _stocksService.GetSellOrders()
            };


            return new ViewAsPdf("OrdersPDF",orders)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins(20, 10, 20, 10),
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                FileName = "Orders.pdf"
            };

        }
    }
}
