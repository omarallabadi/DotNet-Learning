using StockApp_V4.DTO;

namespace StockApp_V4.Models
{
    public class OrdersViewModel
    {

        public List<BuyOrderResponse>? BuyOrders { get; set; }
        public List<SellOrderResponse>? SellOrders { get; set; }
    }
}
