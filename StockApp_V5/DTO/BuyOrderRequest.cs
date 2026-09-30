using StockApp_V5.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;

namespace StockApp_V5.DTO
{
    public class BuyOrderRequest
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

        public BuyOrder ToBuyOrder()
        {
            return new BuyOrder()
            {
                BuyOrderID = Guid.NewGuid(),
                StockSymbol = StockSymbol,
                StockName = StockName,
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Quantity = Quantity,
                Price = Price
            };
        }
    }
}

 
