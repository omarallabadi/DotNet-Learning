using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Rotativa.AspNetCore;
using StockApp_V6.Controllers;
using StockApp_V6.DTO;
using StockApp_V6.Models;
using StockApp_V6.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockTest
{

    public class NullObjectModelValidator : IObjectModelValidator
    {
        public void Validate(
            ActionContext actionContext,
            ValidationStateDictionary? validationState,
            string? prefix,
            object? model)
        {
        }
    }
    public class TradeControllerTest
    {

        private readonly IFinnhubService _myService;
        private readonly IStocksService _stocksService;
        private readonly Mock<IFinnhubService> _FunhubServiceMock;
        private readonly Mock<IStocksService> _StocksServiceMock;
        private readonly IFixture _fixture;
        private readonly Mock<ILogger<TradeController>> _loggerMock;
        private readonly ILogger<TradeController> _logger;
        public TradeControllerTest()
        {
            _FunhubServiceMock = new Mock<IFinnhubService>();
            _StocksServiceMock = new Mock<IStocksService>();
            _stocksService= _StocksServiceMock.Object;
            _myService = _FunhubServiceMock.Object;
            _fixture = new Fixture();
            _loggerMock = new Mock<ILogger<TradeController>>();
            _logger = _loggerMock.Object;

        }
        [Fact]

        public async Task Index_ShouldReturnStock()
        {

            TradeController tradeController = new TradeController(_myService, _stocksService, Options.Create(new TradingOptions { DefaultOrderQuantity = 10 }),_logger);
            _FunhubServiceMock.Setup(repo => repo.getStockPriceQuote("MSFT")).ReturnsAsync(new Dictionary<string, object> { { "c", 150 } });
            _FunhubServiceMock.Setup(repo => repo.GetCompanyProfile("MSFT")).ReturnsAsync(new Dictionary<string, object> { { "name", "Microsoft" } });
            IActionResult result = await tradeController.Index("MSFT");
         


            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            StockTrade stockTrade = Assert.IsType<StockTrade>(viewResult.ViewData.Model);
            stockTrade.StockSymbol.Should().Be("MSFT");
            stockTrade.StockName.Should().Be("Microsoft");
            stockTrade.Price.Should().Be(150);
            stockTrade.Quantity.Should().Be(10);



        }
        [Fact]
        public async Task Orders_ShouldReturnOrders()
        {
            List<BuyOrderResponse> buyOrders = _fixture.Create<List<BuyOrderResponse>>();
            List<SellOrderResponse> sellOrders = _fixture.Create<List<SellOrderResponse>>();

            TradeController tradeController = new TradeController(_myService, _stocksService, Options.Create(new TradingOptions { DefaultOrderQuantity = 10 }),_logger);
            _StocksServiceMock.Setup(repo => repo.GetBuyOrders()).ReturnsAsync(buyOrders);
            _StocksServiceMock.Setup(repo => repo.GetSellOrders()).ReturnsAsync(sellOrders);

            IActionResult result = await tradeController.Orders();



            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            OrdersViewModel ordersViewModel = Assert.IsType<OrdersViewModel>(viewResult.ViewData.Model);
            ordersViewModel.SellOrders.Should().BeEquivalentTo(sellOrders);
            ordersViewModel.BuyOrders.Should().BeEquivalentTo(buyOrders);


        }

        [Fact]
        public async Task BuyOrder_ShouldRedirectToOrders()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                        .With(temp => temp.Quantity, 100u)
                        .With(temp => temp.Price, 100.0)
                        .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                        .Create();
            BuyOrderResponse buyOrderResponse = buyOrderRequest.ToBuyOrder().ToBuyOrderResponse();





            TradeController tradeController = new TradeController(_myService, _stocksService, Options.Create(new TradingOptions { DefaultOrderQuantity = 10 }),_logger);
            tradeController.ObjectValidator = new NullObjectModelValidator();
            _StocksServiceMock.Setup(repo => repo.CreateBuyOrder(buyOrderRequest)).ReturnsAsync(buyOrderResponse);

            IActionResult result = await tradeController.BuyOrder(buyOrderRequest);

            



            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ActionName.Should().Be("Orders");
            _StocksServiceMock.Verify(s => s.CreateBuyOrder(buyOrderRequest), Times.Once);

            ;


        }
      /*  [Fact]
        public async Task BuyOrder_ModelErrors_ShouldReturnIndexViewWithErrors()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                               .With(temp => temp.Quantity, 100u)
                               .With(temp => temp.Price, 100.0)
                               .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                               .Create(); TradeController tradeController = new TradeController(_myService, _stocksService,
                Options.Create(new TradingOptions { DefaultOrderQuantity = 10 }));
            tradeController.ObjectValidator = new NullObjectModelValidator();
            tradeController.ModelState.AddModelError("Quantity", "Invalid quantity");

            // Act
            IActionResult result = await tradeController.BuyOrder(buyOrderRequest);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            _StocksServiceMock.Verify(s => s.CreateBuyOrder(It.IsAny<BuyOrderRequest>()), Times.Never);
        }*/
        [Fact]

        public async Task SellOrder_ShouldRedirectToOrders()
        {
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                        .With(temp => temp.Quantity, 100u)
                        .With(temp => temp.Price, 100.0)
                        .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                        .Create();
            SellOrderResponse sellOrderResponse = sellOrderRequest.ToSellOrder().ToSellOrderResponse();





            TradeController tradeController = new TradeController(_myService, _stocksService, Options.Create(new TradingOptions { DefaultOrderQuantity = 10 }),_logger);
            tradeController.ObjectValidator = new NullObjectModelValidator();
            _StocksServiceMock.Setup(repo => repo.CreateSellOrder(sellOrderRequest)).ReturnsAsync(sellOrderResponse);

            IActionResult result = await tradeController.SellOrder(sellOrderRequest);





            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ActionName.Should().Be("Orders");
            _StocksServiceMock.Verify(s => s.CreateSellOrder(sellOrderRequest), Times.Once);

            ;


        }
        /*  [Fact]

          public async Task Sell_ModelErrors_ShouldReturnIndexViewWithErrors()
          {
              // Arrange
              SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                                 .With(temp => temp.Quantity, 100u)
                                 .With(temp => temp.Price, 100.0)
                                 .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("2024-01-01"))
                                 .Create(); TradeController tradeController = new TradeController(_myService, _stocksService,
                  Options.Create(new TradingOptions { DefaultOrderQuantity = 10 }));
              tradeController.ObjectValidator = new NullObjectModelValidator();
              tradeController.ModelState.AddModelError("Quantity", "Invalid quantity");

              // Act
              IActionResult result = await tradeController.SellOrder(sellOrderRequest);

              // Assert
              ViewResult viewResult = Assert.IsType<ViewResult>(result);

              _StocksServiceMock.Verify(s => s.CreateSellOrder(It.IsAny<SellOrderRequest>()), Times.Never);
          }*/

        
                [Fact]
        public async Task OrdersPDF_ShouldReturnPdf()
        {
            List<BuyOrderResponse> buyOrders = _fixture.Create<List<BuyOrderResponse>>();
            List<SellOrderResponse> sellOrders = _fixture.Create<List<SellOrderResponse>>();

            TradeController tradeController = new TradeController(_myService, _stocksService, Options.Create(new TradingOptions { DefaultOrderQuantity = 10 }),_logger);
            _StocksServiceMock.Setup(repo => repo.GetBuyOrders()).ReturnsAsync(buyOrders);
            _StocksServiceMock.Setup(repo => repo.GetSellOrders()).ReturnsAsync(sellOrders);

            IActionResult result = await tradeController.OrdersPDF();


            ViewAsPdf pdfResult = Assert.IsType<ViewAsPdf>(result);

            Assert.Equal("OrdersPDF", pdfResult.ViewName);
            Assert.Equal("Orders.pdf", pdfResult.FileName);
            Assert.IsType<OrdersViewModel>(pdfResult.Model);

            _StocksServiceMock.Verify(
                s => s.GetBuyOrders(),
                Times.Once);

            _StocksServiceMock.Verify(
                s => s.GetSellOrders(),
                Times.Once);
        }

        public async Task Explore_ShouldReturn25Stocks()
        {

            string[] top25Symbols = Enumerable.Range(1, 25).Select(i => $"SYM{i}").ToArray();

            List<Dictionary<string, string>> stocks = Enumerable.Range(1, 30)
                 .Select(i => new Dictionary<string, string>
                 {
            { "symbol", $"SYM{i}" },
            { "description", $"Company {i}" }
                 })
                 .ToList(); _FunhubServiceMock.Setup(repo => repo.GetStocks()).ReturnsAsync(stocks);

            StocksController stockController = new StocksController(
    _myService,
    Options.Create(new TradingOptions { Top25PopularStocks = string.Join(",", top25Symbols) }));

            IActionResult result = await stockController.Explore(null);



            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            List<Stock> allstocks = Assert.IsType<List<Stock>>(viewResult.ViewData.Model);
            allstocks.Should().HaveCount(25);
            _FunhubServiceMock.Verify(
             s => s.GetStocks(),
             Times.Once);


        }




    }



}
