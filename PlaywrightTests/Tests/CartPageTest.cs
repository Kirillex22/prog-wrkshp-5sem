using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
namespace PlaywrightTests;

[TestClass]
public class CartPageTest : PageTest
{
    private CartPage _cartPage;

    [TestInitialize]
    public void CreatePage()
    {
        _cartPage = new CartPage(Page);
    }

    [TestMethod]
    public async Task SuccesfullAddOneItemToCart()
    {
        var itemName = "Sauce Labs Backpack"; //можно вставить название любого предмета на выбор
        await _cartPage.GotoAsync();
        await _cartPage.Auth();
        await _cartPage.AddItemToCart(itemName);
        await _cartPage.GotoCart();
        var resultName = await _cartPage.GetCartItemName();
        var count = int.Parse(await _cartPage.GetCartItemsCount());
        Assert.IsTrue(resultName.Equals(itemName) && count == 1);
    }
}