namespace StockApp_V1.ServicesContracts
{
    public interface IFinnhubService
    {
        public  Task <Dictionary <string,object>> getStockPriceQuote(string sympol,string token);
        public Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol,string token);

    }
}
