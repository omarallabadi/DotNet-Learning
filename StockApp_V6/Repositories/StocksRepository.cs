using StockApp_V6.Models;
using StockApp_V6.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;


namespace StockApp_V6.Repositories
{
   
    public class StocksRepository : IStocksRepository
    {

        private readonly StockMarketDbContext _db;
        public StocksRepository(StockMarketDbContext db)
        {
            _db = db;
        }


        public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
        {
            _db.BuyOrders.Add(buyOrder);
            await _db.SaveChangesAsync();
            return buyOrder;
        }

        public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
        {
            _db.SellOrders.Add(sellOrder);
            await _db.SaveChangesAsync();
            return sellOrder;
        }

        public async Task<List<BuyOrder>> GetBuyOrders()
        {
            return await _db.BuyOrders.ToListAsync();
        }

        public async Task<List<SellOrder>> GetSellOrders()
        {
            return await _db.SellOrders.ToListAsync();
        }
    }
}
