using Microsoft.Playwright;
using static PlaywrightTests.AuthConstants;
using static PlaywrightTests.SauceLocators;

namespace PlaywrightTests;

public class CartPage : BasePage
{
    public CartPage(IPage page) : base(page) { }

    public async Task Auth()
    {
        await _page.Locator(USERNAME_FIELD).FillAsync(STD_USER);
        await _page.Locator(PASSWORD_FIELD).FillAsync(RIGHT_PSWRD);
        await _page.Locator(LOGIN_BUTTON).ClickAsync();
    }

    public async Task AddItemToCart(string name)
    {
        await _page.Locator(GetCardItemLocator(name)).ClickAsync();
    }

    public async Task GotoCart()
    {
        await _page.Locator(CART_BUTTON).ClickAsync();
    }

    public Task<string> GetCartItemName()
    {
        return _page.Locator(CART_ITEM_NAME).InnerTextAsync();
    }

    public Task<string> GetCartItemsCount()
    {
        return _page.Locator(CURRENT_CART_ITEMS_COUNT).InnerTextAsync();
    }
}