using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace StockApp_V1.Models
{
    public class StockTrade
    {

        public string? StockSymbol { get; set; }
        public  string? StockName { get; set; }
        public  double Price { get; set; }
        public int Quantity { get; set; }



        






    }
}
