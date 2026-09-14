using StockApp_V3.DTO;

namespace StockApp_V3.Models
{
    public class OrdersViewModel
    {

        public List<BuyOrderResponse>? BuyOrders { get; set; }
        public List<SellOrderResponse>? SellOrders { get; set; }
    }
}
