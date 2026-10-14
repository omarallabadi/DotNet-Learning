using AutoFixture;
using Moq;
using StockApp_V6.DTO;
using StockApp_V6.RepositoriesContracts;
using StockApp_V6.Services;
using StockApp_V6.ServicesContracts;
using FluentAssertions;
using StockApp_V6.Models;

namespace StockTest
{
    public class ServiceTest
    {
        private readonly IStocksService _StockServices;
        private readonly IStocksRepository _StockRepository;
        private readonly Mock<IStocksRepository> _stocksRepositoryMock;
        private readonly IFixture _fixture;



        public ServiceTest()
        {
            _stocksRepositoryMock = new Mock<IStocksRepository>();
            _StockRepository = _stocksRepositoryMock.Object;
          _fixture = new Fixture();
            _StockServices = new StockServices(_StockRepository);

        }

        #region CreateBuyOrder

        [Fact]
        public async Task CreateBuyOrder_NullRequest()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = null;

            // Act & Assert
            Func<Task> action = async () => await _StockServices.CreateBuyOrder(buyOrderRequest);

          await  action.Should().ThrowAsync<ArgumentNullException>();

         
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_ZeroQuantity(uint quantity)
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, quantity)
                .With(temp => temp.Price, 100.0)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                .Create();

            // Act & Assert

            Func<Task> action = async () => await _StockServices.CreateBuyOrder(buyOrderRequest);
          await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(100001)]
        public async Task CreateBuyOrder_OverQuantity(uint quantity)
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, quantity)
                .With(temp => temp.Price, 100.0)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                .Create();

    
            // Act & Assert

            Func<Task> action = async () => await _StockServices.CreateBuyOrder(buyOrderRequest);
           await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_ZeroPrice(double price)
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, 100u)
                .With(temp => temp.Price, price)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                .Create();
      

            // Act & Assert
            Func<Task> action = async () => await _StockServices.CreateBuyOrder(buyOrderRequest);
            await action.Should().ThrowAsync<ArgumentException>();
        }

            [Theory]
            [InlineData(10001)]
            public async Task CreateBuyOrder_OverPrice(double price)
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, 100u)
                .With(temp => temp.Price, price)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                .Create();

            // Act & Assert


            Func<Task> action = async () => await _StockServices.CreateBuyOrder(buyOrderRequest);
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_NullSymbol()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, 100u)
                .With(temp => temp.Price, 100.0)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                .With(temp => temp.StockSymbol, null as string)
                .Create();

            // Act & Assert
            Func<Task> action = async () => await _StockServices.CreateBuyOrder(buyOrderRequest);
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_OldDate()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, 100u)
                .With(temp => temp.Price, 100.0)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("1999-12-31"))
                .Create();
            // Act & Assert
            Func<Task> action = async () => await _StockServices.CreateBuyOrder(buyOrderRequest);
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_SupplyAllValues()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, 100u)
                .With(temp => temp.Price, 100.0)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                .Create();

            // Act
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder(); ;
            BuyOrderResponse expected=buyOrder.ToBuyOrderResponse();
            _stocksRepositoryMock.Setup(repo => repo.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);



            BuyOrderResponse response = await _StockServices.CreateBuyOrder(buyOrderRequest);
            expected.BuyOrderID = response.BuyOrderID; // Set the expected BuyOrderID to match the response 

            // Assert
            response.BuyOrderID.Should().NotBe(Guid.Empty);

            response.Should().BeEquivalentTo(expected);
        }

        #endregion


        #region CreateSellOrder

        [Fact]
        public async Task CreateSellOrder_NullRequest()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = null;

            // Act & Assert
            Func<Task> action = async () => await _StockServices.CreateSellOrder(sellOrderRequest);


            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_ZeroQuantity(uint quantity)
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, quantity)
                .With(temp => temp.Price, 100.0)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                .Create();

            // Act & Assert
            Func<Task> action = async () => await _StockServices.CreateSellOrder(sellOrderRequest);


            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(100001)]
        public async Task CreateSellOrder_OverQuantity(uint quantity)
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, quantity)
                .With(temp => temp.Price, 100.0)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                .Create();
          

            // Act & Assert
            Func<Task> action = async () => await _StockServices.CreateSellOrder(sellOrderRequest);
            await action.Should().ThrowAsync<ArgumentException>();


           
        }

        [Theory]
        [InlineData(0)]
        public async Task CreateSellOrder_ZeroPrice(double price)
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, 100u)
                .With(temp => temp.Price, price)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                .Create();
          

            // Act & Assert
            Func<Task> action = async () => await _StockServices.CreateSellOrder(sellOrderRequest);
            await action.Should().ThrowAsync<ArgumentException>();


      

   
        }

        [Theory]
        [InlineData(10001)]
        public async Task CreateSellOrder_OverPrice(double price)
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, 100u)
                .With(temp => temp.Price, price)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                .Create();
          

            // Act & Assert
            Func<Task> action = async () => await _StockServices.CreateSellOrder(sellOrderRequest);
            await action.Should().ThrowAsync<ArgumentException>();

        }

        [Fact]
        public async Task CreateSellOrder_NullSymbol()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, 100u)
                .With(temp => temp.Price, 100.0)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                .With(temp => temp.StockSymbol, null as string)
                .Create();

            // Act & Assert
           Func<Task> action = async () => await _StockServices.CreateSellOrder(sellOrderRequest);
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData("1999-12-31")]
        public async Task CreateSellOrder_OldDate(string date)
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, 100u)
                .With(temp => temp.Price, 100.0)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse(date))
                .Create();

            // Act & Assert
            Func<Task> action = async () => await _StockServices.CreateSellOrder(sellOrderRequest);
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_SupplyAllValues()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, 100u)
                .With(temp => temp.Price, 100.0)
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                .Create();

            // Act
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            SellOrderResponse expected = sellOrder.ToSellOrderResponse();
            _stocksRepositoryMock.Setup(repo => repo.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);

            SellOrderResponse response = await _StockServices.CreateSellOrder(sellOrderRequest);
            expected.SellOrderID = response.SellOrderID; // Set the expected SellOrderID to match the response
            response.SellOrderID.Should().NotBe(Guid.Empty);
            response.Should().BeEquivalentTo(expected);

        }

        #endregion


        #region GetAllBuyOrders

        [Fact]
        public async Task GetAllBuyOrders_EmptyList()
        {
            //arrange
            List<BuyOrder> buyOrders = new List<BuyOrder>();

            _stocksRepositoryMock.Setup(repo => repo.GetBuyOrders())
                .ReturnsAsync(buyOrders);

            //act
            List<BuyOrderResponse> response = await _StockServices.GetBuyOrders();
            //assert
            response.Should().BeEmpty();
        }
                

        [Fact]
        public async Task GetAllBuyOrders_WithExistingOrders()
        {
        //arramge      
        List<BuyOrder>buyOrders=_fixture.CreateMany<BuyOrder>(5).ToList();

            _stocksRepositoryMock.Setup(repo => repo.GetBuyOrders())
                .ReturnsAsync(buyOrders);


            List<BuyOrderResponse> expected = buyOrders.Select(order => order.ToBuyOrderResponse()).ToList();
            //act
            List<BuyOrderResponse> response = await _StockServices.GetBuyOrders();
            //assert
            response.Should().BeEquivalentTo(expected);

        }

        #endregion



        #region GetAllSellOrders

        [Fact]
        public async Task GetAllSellOrders_EmptyList()
        {
            //arrange
            List<SellOrder> sellOrders = new List<SellOrder>();

            _stocksRepositoryMock.Setup(repo => repo.GetSellOrders())
                .ReturnsAsync(sellOrders);

            //act
            List<SellOrderResponse> response = await _StockServices.GetSellOrders();
            //assert
            response.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllSellOrders_WithExistingOrders()
        {
            //arrange
            List<SellOrder> sellOrders = _fixture.CreateMany<SellOrder>(5).ToList();

            _stocksRepositoryMock.Setup(repo => repo.GetSellOrders())
                .ReturnsAsync(sellOrders);


            List<SellOrderResponse> expected = sellOrders.Select(order => order.ToSellOrderResponse()).ToList();
            //act
            List<SellOrderResponse> response = await _StockServices.GetSellOrders();
            //assert
            response.Should().BeEquivalentTo(expected);
        }

        #endregion
    }
}
