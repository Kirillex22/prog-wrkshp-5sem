using System.Threading.Tasks;
using Microsoft.Playwright;
using static PlaywrightTests.SauceLocators;
namespace PlaywrightTests;

class MainPage : BasePage
{
    public MainPage(IPage page) : base(page) { }

    public async Task FillUsername(string username)
    {
        await _page.Locator(USERNAME_FIELD).FillAsync(username);
    }

    public async Task FillPassword(string password)
    {
        await _page.Locator(PASSWORD_FIELD).FillAsync(password);
    }

    public async Task ClickLoginButton()
    {
        await _page.Locator(LOGIN_BUTTON).ClickAsync();
    }

    public ILocator GetErrorMessageLocator()
    {
        return _page.Locator(ERROR_MESSAGE);
    }

}