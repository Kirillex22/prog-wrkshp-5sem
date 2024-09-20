using Microsoft.Playwright;
using static PlaywrightTests.SauceLocators;
using static PlaywrightTests.AuthConstants;
namespace PlaywrightTests;

class MarketPage : BasePage
{
    public MarketPage(IPage page) : base(page) { }

    public async Task Auth()
    {
        await _page.Locator(USERNAME_FIELD).FillAsync(STD_USER);
        await _page.Locator(PASSWORD_FIELD).FillAsync(RIGHT_PSWRD);
        await _page.Locator(LOGIN_BUTTON).ClickAsync();
    }


    public async Task SortPrice(string mode = "hilo")
    {
        await _page.Locator(SORT_OPTIONS).SelectOptionAsync(new[] { mode });
    }

    public async Task<string[]> GetPrices()
    {
        var count = await _page.Locator(PRICE_LOCATOR).CountAsync();
        var a = new List<string>();
        for (int i = 0; i < count; i++)
        {
            a.Add(await _page.Locator(PRICE_LOCATOR).Nth(i).InnerTextAsync());
        }
        return a.ToArray();
    }
}