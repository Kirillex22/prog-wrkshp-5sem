using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using static PlaywrightTests.MainPageSauceConstants;
using static PlaywrightTests.AuthConstants;
namespace PlaywrightTests;

//pwsh bin/Debug/net8.0/playwright.ps1 codegen https://www.saucedemo.com/
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
    public async Task SuccesfullAuth()
    {
        await _marketPage.GotoAsync();
        await _marketPage.Auth();
    }
}
//dotnet test --logger "html;logfilename=testResults.html"