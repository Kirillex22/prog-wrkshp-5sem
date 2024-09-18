using System.Threading.Tasks;
using Microsoft.Playwright;
using static PlaywrightTests.MainPageLocators;
namespace PlaywrightTests;

class MainPage : BasePage
{
    public MainPage(IPage page) : base(page)
    {
    }

    public async Task fillUsername(string username)
    {
        await _page.Locator(USERNAME_FIELD).FillAsync(username);
    }

    public async Task fillPassword(string password)
    {
        await _page.Locator(PASSWORD_FIELD).FillAsync(password);
    }

    public async Task clickLoginButton()
    {
        await _page.Locator(LOGIN_BUTTON).ClickAsync();
    }

    public ILocator getErrorMessageLocator()
    {
        return _page.Locator(ERROR_MESSAGE);
    }

}//await Expect(page.Locator("[data-test=\"error\"]")).ToContainTextAsync("Epic sadface: Username and password do not match any user in this service");