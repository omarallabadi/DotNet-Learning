using StockApp_V1.ServicesContracts;
using System.Text.Json;

namespace StockApp_V1.Services
{
    public class MyService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MyService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string sympol,string token)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={sympol}&token={token}"),
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

        public async Task<Dictionary<string, object>> getStockPriceQuote(string sympol,string token)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={sympol}&token={token}"),
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
    }
}
