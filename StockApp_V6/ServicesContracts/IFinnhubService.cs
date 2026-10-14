namespace StockApp_V6.ServicesContracts
{
    public interface IFinnhubService
    {
        public  Task <Dictionary <string,object>>? getStockPriceQuote(string sympol);
        public Task<Dictionary<string, object>>? GetCompanyProfile(string stockSymbol);
        public Task<List<Dictionary<string, string>>>? GetStocks();


    }
}


