namespace StockApp_V6.RepositoriesContracts
{
    public interface IFinnhubRepository
    {
        public Task<List<Dictionary<string, string>>> GetStocks();

        public Task<Dictionary<string, object>> GetCompanyProfile(string sympol);

        public Task<Dictionary<string, object>> GetStockQuote(string sympol);
         public Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}
