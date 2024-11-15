using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Text.Json;
using static Data;

namespace RestfulBookerTests;

[TestClass]
public class AuthTest : PlaywrightTest
{
    private IAPIRequestContext _baseRequest = null!;

    [TestInitialize]
    public async Task SetUpAPITesting()
    {
        await CreateAPIRequestContext();
    }

    private async Task CreateAPIRequestContext()
    {
        _baseRequest = await Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = baseUrl,
            ExtraHTTPHeaders = new Dictionary<string, string>(){
                {"Content-Type", "application/json"}
            }
        });
    }

    [TestMethod]
    public async Task SuccesfullAuth()
    {
        var authRequest = new AuthRequest(_baseRequest);

        var data = new Dictionary<string, object>(){
            {"username", username},
            {"password", password}
        };

        var response = await authRequest.Send(data);
        var jsonDocument = JsonDocument.Parse(await response.TextAsync());
        await Expect(response).ToBeOKAsync();
        Assert.IsTrue(jsonDocument.RootElement.TryGetProperty("token", out var token));
        Data.token = token.GetString();
    }

    [TestCleanup]
    public async Task TearDownAPITesting()
    {
        await _baseRequest.DisposeAsync();
    }
}