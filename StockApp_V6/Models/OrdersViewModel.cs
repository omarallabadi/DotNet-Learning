using StockApp_V6.DTO;

namespace StockApp_V6.Models
{
    public class OrdersViewModel
    {

        public List<BuyOrderResponse>? BuyOrders { get; set; }
        public List<SellOrderResponse>? SellOrders { get; set; }
    }
}
