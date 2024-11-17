using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

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
            BaseURL = Data.baseUrl,
            ExtraHTTPHeaders = Data.authHeaders
        });
    }

    [TestMethod]
    public async Task SuccesfullAuth()
    {
        //create and send request
        var authRequest = new AuthRequest(_baseRequest);

        var data = new Dictionary<string, object>(){
            {"username", Data.username},
            {"password", Data.password}
        };

        var response = await authRequest.Send(data);
        var jsonData = await response.TextAsync();

        //check status code
        await Expect(response).ToBeOKAsync();

        //schema validating
        var json = JObject.Parse(jsonData);
        Assert.IsTrue(json.IsValid(Data.schemas["Auth"]));

        //check content
        var jsonDocument = JsonDocument.Parse(jsonData);
        Assert.IsTrue(jsonDocument.RootElement.TryGetProperty("token", out var token));

        //set token for next tests
        Data.SetToken(token.GetString());
    }

    [TestCleanup]
    public async Task TearDownAPITesting()
    {
        await _baseRequest.DisposeAsync();
    }
}