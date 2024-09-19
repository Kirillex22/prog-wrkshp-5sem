using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using static PlaywrightTests.MainPageSauceConstants;
using static PlaywrightTests.AuthConstants;
namespace PlaywrightTests;

//pwsh bin/Debug/net8.0/playwright.ps1 codegen https://www.saucedemo.com/
[TestClass]
public class MainPageTest : PageTest
{
    private MainPage _mainPage;

    [TestInitialize]
    public void CreatePage()
    {
        _mainPage = new MainPage(Page);
    }

    [TestMethod]
    public async Task StdUserFillsWrongPassword()
    {
        await _mainPage.GotoAsync();
        await _mainPage.FillUsername(STD_USER);
        await _mainPage.FillPassword(WRONG_PSWRD);
        await _mainPage.ClickLoginButton();
        await Expect(_mainPage.GetErrorMessageLocator()).ToContainTextAsync(STD_USER_ERR_MSG);
    }

    [TestMethod]
    public async Task LckdOutUsrFillsRightPassword()
    {
        await _mainPage.GotoAsync();
        await _mainPage.FillUsername(LCKD_OUT_USER);
        await _mainPage.FillPassword(RIGHT_PSWRD);
        await _mainPage.ClickLoginButton();
        await Expect(_mainPage.GetErrorMessageLocator()).ToContainTextAsync(LO_USER_ERR_MSG);
    }
}
//dotnet test --logger "html;logfilename=testResults.html"