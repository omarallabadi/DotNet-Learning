using Services.Helpers;
using StockApp_V6.DTO;
using StockApp_V6.Models;
using StockApp_V6.ServicesContracts;
using Microsoft.EntityFrameworkCore;
using StockApp_V6.RepositoriesContracts;

namespace StockApp_V6.Services
{
    public class StockServices : IStocksService
    {

        private readonly IStocksRepository _stocksRepository;



        public StockServices(IStocksRepository stocksRepository) {
            _stocksRepository = stocksRepository;
        }
        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if(buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }
            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder=buyOrderRequest.ToBuyOrder();
            await _stocksRepository.CreateBuyOrder(buyOrder);
            BuyOrderResponse buyOrderResponse=buyOrder.ToBuyOrderResponse();
            return buyOrderResponse;
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }
            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            await _stocksRepository.CreateSellOrder(sellOrder);
            SellOrderResponse sellOrderResponse = sellOrder.ToSellOrderResponse();
            return sellOrderResponse;
        }
       
     

        public async   Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            var buyOrders = await _stocksRepository.GetBuyOrders();
            return buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            var sellOrders = await _stocksRepository.GetSellOrders();
            return sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
        }
        
    }
}
