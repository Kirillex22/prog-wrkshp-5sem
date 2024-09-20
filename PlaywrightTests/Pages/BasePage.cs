using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightTests;

public class BasePage
{
    protected readonly IPage _page;
    private string _baseUrl = "https://www.saucedemo.com/";
    public BasePage(IPage page)
    {
        _page = page;
    }

    public async Task GotoAsync()
    {
        await _page.GotoAsync(_baseUrl);
    }
}