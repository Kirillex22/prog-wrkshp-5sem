using Microsoft.Playwright.MSTest;
namespace PlaywrightTests;

[TestClass]
public class MarketPageTest : PageTest
{
    private MarketPage _marketPage;

    [TestInitialize]
    public void CreatePage()
    {
        _marketPage = new MarketPage(Page);
    }

    [TestMethod]
    public async Task SuccesfullSortingByPriceHiLo()
    {
        await _marketPage.GotoAsync();
        await _marketPage.Auth();
        await _marketPage.SortPrice();

        var prices = await _marketPage.GetPrices();
        var expectedList = prices.OrderByDescending(x => float.Parse(x.Trim('$').Replace('.', ',')));
        Assert.IsTrue(expectedList.SequenceEqual(prices));
    }
}
