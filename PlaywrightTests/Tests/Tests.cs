using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using static PlaywrightTests.SauceConstants;
namespace PlaywrightTests;

[TestClass]
public class ExampleTest : PageTest
{
    [TestMethod]
    public async Task FillingWrongPassword()
    {
        var mainPage = new MainPage(await Context.NewPageAsync());
        await mainPage.GotoAsync();
        await mainPage.fillUsername("standart_user");
        await mainPage.fillPassword("wrong_password");
        await mainPage.clickLoginButton();
        await Expect(mainPage.getErrorMessageLocator()).ToContainTextAsync(MAIN_PAGE_LOGIN_ERROR_MESSAGE);
    }

}