
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
        private readonly ILogger<TradeController> _logger;

        public TradeController(
            IFinnhubService service,
            IStocksService stocksService,
            IOptions<TradingOptions> options,
            ILogger<TradeController> logger)
        {
            _myService = service;
            _stocksService = stocksService;
            _options = options.Value;
            _logger = logger;

            _logger.LogInformation("TradeController initialized");
        }

        [Route("/")]
        [Route("[action]")]
        public async Task<IActionResult> Index(string? stock)
        {
            _logger.LogInformation("Index action started. Stock: {Stock}", stock);

            string sympol = stock ?? "MSFT";

            _logger.LogDebug("Fetching quote and company profile for {Stock}", sympol);

            Dictionary<string, object> response1 =
                await _myService.getStockPriceQuote(sympol);

            Dictionary<string, object> response2 =
                await _myService.GetCompanyProfile(sympol);

            StockTrade resultStock = new StockTrade()
            {
                StockSymbol = Convert.ToString(sympol),
                StockName = Convert.ToString(response2["name"]),
                Price = Convert.ToDouble(Convert.ToString(response1["c"])),
                Quantity = _options.DefaultOrderQuantity
            };

            _logger.LogInformation(
                "Stock information loaded successfully. Stock: {Stock}, Price: {Price}",
                resultStock.StockSymbol,
                resultStock.Price);

            return View(resultStock);
        }

        [Route("[action]")]
        public async Task<IActionResult> Orders()
        {
            _logger.LogInformation("Orders action started");

            List<BuyOrderResponse> buyOrderResponses =
                await _stocksService.GetBuyOrders();

            List<SellOrderResponse> sellOrderResponses =
                await _stocksService.GetSellOrders();

            OrdersViewModel orders = new OrdersViewModel()
            {
                BuyOrders = buyOrderResponses,
                SellOrders = sellOrderResponses
            };

            _logger.LogInformation(
                "Orders loaded successfully. Buy orders: {BuyCount}, Sell orders: {SellCount}",
                buyOrderResponses.Count,
                sellOrderResponses.Count);

            return View(orders);
        }

        [Route("[action]")]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            _logger.LogInformation(
                "BuyOrder action started for stock {StockSymbol}",
                buyOrderRequest.StockSymbol);

            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            ModelState.Clear();
            TryValidateModel(buyOrderRequest);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning(
                    "Buy order validation failed for stock {StockSymbol}",
                    buyOrderRequest.StockSymbol);

                ViewBag.errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                StockTrade stockTrade = new StockTrade()
                {
                    StockName = buyOrderRequest.StockName,
                    StockSymbol = buyOrderRequest.StockSymbol,
                    Price = buyOrderRequest.Price
                };

                return View("index", stockTrade);
            }

            await _stocksService.CreateBuyOrder(buyOrderRequest);

            _logger.LogInformation(
                "Buy order created successfully for stock {StockSymbol}",
                buyOrderRequest.StockSymbol);

            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
        {
            _logger.LogInformation(
                "SellOrder action started for stock {StockSymbol}",
                sellOrderRequest.StockSymbol);

            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            ModelState.Clear();
            TryValidateModel(sellOrderRequest);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning(
                    "Sell order validation failed for stock {StockSymbol}",
                    sellOrderRequest.StockSymbol);

                ViewBag.errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                StockTrade stockTrade = new StockTrade()
                {
                    StockName = sellOrderRequest.StockName,
                    StockSymbol = sellOrderRequest.StockSymbol,
                    Price = sellOrderRequest.Price
                };

                return View("index", stockTrade);
            }

            await _stocksService.CreateSellOrder(sellOrderRequest);

            _logger.LogInformation(
                "Sell order created successfully for stock {StockSymbol}",
                sellOrderRequest.StockSymbol);

            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {
            _logger.LogInformation("OrdersPDF action started");

            OrdersViewModel orders = new OrdersViewModel()
            {
                BuyOrders = await _stocksService.GetBuyOrders(),
                SellOrders = await _stocksService.GetSellOrders()
            };

            _logger.LogInformation("Orders PDF generated successfully");

            return new ViewAsPdf("OrdersPDF", orders)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins(20, 10, 20, 10),
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                FileName = "Orders.pdf"
            };
        }
    }
}
