using Services.Helpers;
using StockApp_V5.DTO;
using StockApp_V5.Models;
using StockApp_V5.ServicesContracts;
using Microsoft.EntityFrameworkCore;

namespace StockApp_V5.Services
{
    public class StockServices : IStocksService
    {
private readonly StockMarketDbContext _db;



        public StockServices(StockMarketDbContext db) {
            _db = db;

        }
        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if(buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }
            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder=buyOrderRequest.ToBuyOrder();
            _db.BuyOrders.Add(buyOrder);
            await _db.SaveChangesAsync();
            BuyOrderResponse buyOrderResponse=buyOrder.ToBuyOrderResponse();
            return buyOrderResponse;
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }
            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _db.SellOrders.Add(sellOrder);
            await _db.SaveChangesAsync();
            SellOrderResponse sellOrderResponse = sellOrder.ToSellOrderResponse();
            return sellOrderResponse;
        }

        public async   Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            var buyOrders = await _db.BuyOrders.ToListAsync();
            return buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            var sellOrders = await _db.SellOrders.ToListAsync();
            return sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
        }
        
    }
}
