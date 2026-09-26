using StockApp_V4.Models;
using System.ComponentModel.DataAnnotations;

namespace StockApp_V4.DTO
{
    public class SellOrderRequest
    {
        [Required]
        public string StockSymbol { get; set; } = string.Empty;

        [Required]
        public string StockName { get; set; } = string.Empty;

        [Range(typeof(DateTime), "2000-01-01", "9999-12-31")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public uint Quantity { get; set; }

        [Range(1, 10000)]
        public double Price { get; set; }

        public SellOrder ToSellOrder()
        {
            return new SellOrder()
            {
                SellOrderID = Guid.NewGuid(),
                StockSymbol = StockSymbol,
                StockName = StockName,
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Quantity = Quantity,
                Price = Price
            };
        }
    }
}
