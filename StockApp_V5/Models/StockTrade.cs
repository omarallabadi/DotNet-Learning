using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace StockApp_V5.Models
{
    public class StockTrade
    {

        public string? StockSymbol { get; set; }
        public  string? StockName { get; set; }
        public  double Price { get; set; }
        [Range(1, 100000, ErrorMessage = "Quantity must be between 1 and 100000")]

        public int Quantity { get; set; }



        






    }
}
