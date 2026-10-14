using StockApp_V6.RepositoriesContracts;
using System.Text.Json;

namespace StockApp_V6.Repositories
{
    public class FinnhubRepository : IFinnhubRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public FinnhubRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }



        public async Task<Dictionary<string, object>> GetCompanyProfile(string sympol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={sympol}&token={_configuration["token"]}"),
                    Method = HttpMethod.Get,
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                Stream stream = httpResponseMessage.Content.ReadAsStream();
                StreamReader reader = new StreamReader(stream);
                string response = reader.ReadToEnd();
                Dictionary<string, object>? responseDic = JsonSerializer.Deserialize<Dictionary<string, object>>(response);


                if (responseDic == null)
                {
                    throw new InvalidOperationException("no response from server");


                }
                else if (responseDic.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(responseDic["error"]));


                }

                return responseDic;
            }
        }

        public async Task<Dictionary<string, object>> GetStockQuote(string sympol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={sympol}&token={_configuration["token"]}"),
                    Method = HttpMethod.Get,
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                Stream stream = httpResponseMessage.Content.ReadAsStream();
                StreamReader reader = new StreamReader(stream);
                string response = reader.ReadToEnd();
                Dictionary<string, object>? responseDic = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if (responseDic == null)
                {
                    throw new InvalidOperationException("no response from server");


                }
                else if (responseDic.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(responseDic["error"]));


                }
                return responseDic;
            }
        }

        public async Task<List<Dictionary<string, string>>> GetStocks()
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={_configuration["token"]}"),
                    Method = HttpMethod.Get,
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                Stream stream = httpResponseMessage.Content.ReadAsStream();
                StreamReader reader = new StreamReader(stream);
                string response = reader.ReadToEnd();
                List<Dictionary<string, string>>? responseList = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(response);


                if (responseList == null)
                {
                    throw new InvalidOperationException("no response from server");

                }
            

                return responseList;
            }
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={_configuration["token"]}"),
                    Method = HttpMethod.Get,
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                Stream stream = httpResponseMessage.Content.ReadAsStream();
                StreamReader reader = new StreamReader(stream);
                string response = reader.ReadToEnd();
               Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(response);


                if (result == null)
                {
                    throw new InvalidOperationException("no response from server");

                }


                return result;
            }

        }
    }
}
