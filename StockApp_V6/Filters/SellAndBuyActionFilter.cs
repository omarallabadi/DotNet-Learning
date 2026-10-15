using StockApp_V6.Controllers;
using StockApp_V6.DTO;
using StockApp_V6.Models;
using Microsoft.AspNetCore.Mvc.Filters;


namespace StockApp_V6.Filters
{
    public class SellAndBuyActionFilter : IAsyncActionFilter
    {

        ILogger<SellAndBuyActionFilter> _logger;

        public SellAndBuyActionFilter(ILogger<SellAndBuyActionFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


            TradeController tradeController = (TradeController)context.Controller;
            SellOrderRequest sellOrderRequest = (SellOrderRequest)context.ActionArguments["sellOrderRequest"];









            _logger.LogInformation(
              "SellOrder action started for stock {StockSymbol}",
              sellOrderRequest.StockSymbol);

            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            tradeController.ModelState.Clear();
            tradeController.TryValidateModel(sellOrderRequest);

            if (!tradeController.ModelState.IsValid)
            {
                _logger.LogWarning(
                    "Sell order validation failed for stock {StockSymbol}",
                    sellOrderRequest.StockSymbol);

                tradeController.ViewBag.errors = tradeController.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                StockTrade stockTrade = new StockTrade()
                {
                    StockName = sellOrderRequest.StockName,
                    StockSymbol = sellOrderRequest.StockSymbol,
                    Price = sellOrderRequest.Price
                };

                
                context.Result = tradeController.View("index", stockTrade);
                return;

            }



            await next();
        }
    }
}
