using CRUDTests;
using Fizzler.Systems.HtmlAgilityPack;
using FluentAssertions;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace StockTest
{
    public class StockControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;


        public StockControllerIntegrationTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }



        #region Index

        [Fact]
        public async Task Index_ToReturnView()
        {
            //arrange



            //act
            HttpResponseMessage response = await _client.GetAsync("Trade/Index");

            //assert
            response.IsSuccessStatusCode.Should().BeTrue();

            string responseBody = await response.Content.ReadAsStringAsync();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(responseBody);

            var document = htmlDocument.DocumentNode;

            document.QuerySelectorAll("form.trade").Should().NotBeEmpty();
        }
    }
        #endregion
}
