using Microsoft.AspNetCore.Mvc;
using StockApp_V6.Models;
using StockApp_V6.ServicesContracts;

public class SelectedStockViewComponent : ViewComponent
{
    private readonly IFinnhubService _myService;

    public SelectedStockViewComponent(
        IFinnhubService myService,
        IConfiguration configuration)
    {
        _myService = myService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string stock)
    {

        Dictionary<string, object> quote =
            await _myService.getStockPriceQuote(stock);

        Dictionary<string, object> profile =
            await _myService.GetCompanyProfile(stock);

        StockTrade selectedStock = new StockTrade()
        {
            StockSymbol = Convert.ToString(stock),
            StockName = Convert.ToString(profile["name"]),
            Price = Convert.ToDouble(Convert.ToString(quote["c"])),
            Logo = Convert.ToString(profile["logo"])
        };

        return View(selectedStock);
    }
}