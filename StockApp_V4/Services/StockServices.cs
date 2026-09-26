using Services.Helpers;
using StockApp_V4.DTO;
using StockApp_V4.Models;
using StockApp_V4.ServicesContracts;

namespace StockApp_V4.Services
{
    public class StockServices : IStocksService
    {
        private readonly List<BuyOrder> buyOrders;
        private readonly List<SellOrder> sellOrders ;



        public StockServices(bool initialize=true) {
            buyOrders = new List<BuyOrder>();
            sellOrders = new List<SellOrder>();

            if (initialize == true)
            {
                buyOrders.AddRange(new List<BuyOrder> { 
                 new BuyOrder
                 {
                     BuyOrderID = Guid.NewGuid(),
                     StockSymbol = "AAPL",
                     StockName = "Apple Inc.",
                     DateAndTimeOfOrder = DateTime.Now,
                     Quantity = 100,
                     Price = 150.50
                 },

    new BuyOrder
    {
        BuyOrderID = Guid.NewGuid(),
        StockSymbol = "MSFT",
        StockName = "Microsoft Corporation",
        DateAndTimeOfOrder = DateTime.Now,
        Quantity = 500,
        Price = 300.25
    },

    new BuyOrder
    {
        BuyOrderID = Guid.NewGuid(),
        StockSymbol = "GOOGL",
        StockName = "Alphabet Inc.",
        DateAndTimeOfOrder = DateTime.Now,
        Quantity = 1000,
        Price = 175.75
    },

    new BuyOrder
    {
        BuyOrderID = Guid.NewGuid(),
        StockSymbol = "AMZN",
        StockName = "Amazon.com Inc.",
        DateAndTimeOfOrder = DateTime.Now,
        Quantity = 2500,
        Price = 180.00
    },

    new BuyOrder
    {
        BuyOrderID = Guid.NewGuid(),
        StockSymbol = "TSLA",
        StockName = "Tesla Inc.",
        DateAndTimeOfOrder = DateTime.Now,
        Quantity = 10000,
        Price = 250.50
    }});

                sellOrders.AddRange(new List<SellOrder>
{
    new SellOrder
    {
        SellOrderID = Guid.NewGuid(),
        StockSymbol = "AAPL",
        StockName = "Apple Inc.",
        DateAndTimeOfOrder = DateTime.Now,
        Quantity = 100,
        Price = 150.50
    },

    new SellOrder
    {
        SellOrderID = Guid.NewGuid(),
        StockSymbol = "MSFT",
        StockName = "Microsoft Corporation",
        DateAndTimeOfOrder = DateTime.Now,
        Quantity = 500,
        Price = 300.25
    },

    new SellOrder
    {
        SellOrderID = Guid.NewGuid(),
        StockSymbol = "GOOGL",
        StockName = "Alphabet Inc.",
        DateAndTimeOfOrder = DateTime.Now,
        Quantity = 1000,
        Price = 175.75
    },

    new SellOrder
    {
        SellOrderID = Guid.NewGuid(),
        StockSymbol = "AMZN",
        StockName = "Amazon.com Inc.",
        DateAndTimeOfOrder = DateTime.Now,
        Quantity = 2500,
        Price = 180.00
    },

    new SellOrder
    {
        SellOrderID = Guid.NewGuid(),
        StockSymbol = "TSLA",
        StockName = "Tesla Inc.",
        DateAndTimeOfOrder = DateTime.Now,
        Quantity = 10000,
        Price = 250.50
    }
});


            }


        }
        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if(buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }
            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder=buyOrderRequest.ToBuyOrder();
            buyOrders.Add(buyOrder);
            BuyOrderResponse buyOrderResponse=buyOrder.ToBuyOrderResponse();
            return Task.FromResult(buyOrderResponse);
        }

        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }
            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrders.Add(sellOrder);
            SellOrderResponse sellOrderResponse = sellOrder.ToSellOrderResponse();
            return Task.FromResult(sellOrderResponse);
        }

        public Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return Task.FromResult(buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList());
        }

        public Task<List<SellOrderResponse>> GetSellOrders()
        {
            return Task.FromResult(sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList());
        }
    }
}
