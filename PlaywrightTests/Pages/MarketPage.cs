using System.Threading.Tasks;
using Microsoft.Playwright;
using static PlaywrightTests.MainPageLocators;
using static PlaywrightTests.AuthConstants;
namespace PlaywrightTests;

class MarketPage : BasePage
{
    public MarketPage(IPage page) : base(page)
    {
    }

    public async Task Auth()
    {
        await _page.Locator(USERNAME_FIELD).FillAsync(STD_USER);
        await _page.Locator(PASSWORD_FIELD).FillAsync(RIGHT_PSWRD);
        await _page.Locator(LOGIN_BUTTON).ClickAsync();
    }
}