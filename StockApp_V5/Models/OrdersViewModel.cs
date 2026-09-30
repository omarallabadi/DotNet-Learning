using StockApp_V5.DTO;

namespace StockApp_V5.Models
{
    public class OrdersViewModel
    {

        public List<BuyOrderResponse>? BuyOrders { get; set; }
        public List<SellOrderResponse>? SellOrders { get; set; }
    }
}
