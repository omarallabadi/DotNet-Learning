using StockApp_V3.Models;
using System.Net;
using System.Reflection;

namespace StockApp_V3.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }

        public string StockSymbol { get; set; } = string.Empty;

        public string StockName { get; set; } = string.Empty;

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(SellOrderResponse))
            {
                return false;
            }

            SellOrderResponse sellOrder = (SellOrderResponse)obj;

            return SellOrderID == sellOrder.SellOrderID &&
                   StockSymbol == sellOrder.StockSymbol &&
                   StockName == sellOrder.StockName &&
                   DateAndTimeOfOrder == sellOrder.DateAndTimeOfOrder &&
                   Quantity == sellOrder.Quantity &&
                   Price == sellOrder.Price &&
                   TradeAmount == sellOrder.TradeAmount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public static class SellOrderExtensions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse()
            {
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
                TradeAmount = sellOrder.Quantity * sellOrder.Price
            };
        }
    }
};
