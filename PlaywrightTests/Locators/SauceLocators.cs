namespace PlaywrightTests;

public static class SauceLocators
{
    public static string USERNAME_FIELD = "[data-test=\"username\"]";
    public static string PASSWORD_FIELD = "[data-test=\"password\"]";
    public static string LOGIN_BUTTON = "[data-test=\"login-button\"]";
    public static string ERROR_MESSAGE = "[data-test=\"error\"]";
    public static string SORT_OPTIONS = "[data-test=\"product-sort-container\"]";
    public static string PRICE_LOCATOR = "[data-test=\"inventory-item-price\"]";
    public static string ADD_TO_CART_BUTTON = "[data-test=\"add-to-cart-sauce-labs-backpack\"]";
    public static string CART_BUTTON = "[data-test=\"shopping-cart-link\"]";
    public static string CART_ITEM_NAME = "[data-test=\"inventory-item-name\"]";
    public static string CURRENT_CART_ITEMS_COUNT = "[data-test=\"item-quantity\"]";
    public static string GetCardItemLocator(string name) => $"[data-test=\"add-to-cart-{string.Join('-', name.ToLower().Split())}\"]";
}