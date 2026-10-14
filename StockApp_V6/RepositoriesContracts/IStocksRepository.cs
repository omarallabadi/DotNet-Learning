using StockApp_V6.Models;

namespace StockApp_V6.RepositoriesContracts
{
    public interface IStocksRepository
    {

        Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);

        Task<SellOrder> CreateSellOrder(SellOrder sellOrder);

        Task<List<BuyOrder>> GetBuyOrders();

        Task<List<SellOrder>> GetSellOrders();
    }
}
