using StockApp_V6.RepositoriesContracts;
using StockApp_V6.ServicesContracts;
using System.Text.Json;
using System.Web.Razor.Tokenizer.Symbols;

namespace StockApp_V6.Services
{
    public class MyService : IFinnhubService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public MyService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string sympol)

        {

            var responseDic = await _finnhubRepository.GetCompanyProfile(sympol);



            return responseDic;
        }

        public async Task<Dictionary<string, object>> getStockPriceQuote(string sympol)
        {
            var responseDic = await _finnhubRepository.GetStockQuote(sympol);
            return responseDic;
        }

        public async Task<List<Dictionary<string, string>>>? GetStocks()
        {
            var responseDic = await _finnhubRepository.GetStocks();
            return responseDic;

        }
        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            var responseDic = await _finnhubRepository.SearchStocks(stockSymbolToSearch);
            return responseDic;
        }
    }
}
