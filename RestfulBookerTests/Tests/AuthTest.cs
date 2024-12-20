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
    [DynamicData(nameof(Data.GetValidAuthCases), typeof(Data), DynamicDataSourceType.Method)]
    public async Task ValidAuth(int expectedcode, Dictionary<string, object> authData, string caseName)
    {
        //create and send request
        var authRequest = new AuthRequest(_baseRequest);
        var response = await authRequest.Send(authData);
        var jsonData = await response.TextAsync();

        //check status code
        Assert.IsTrue(response.Status == expectedcode);

        //schema validating
        var json = JObject.Parse(jsonData);
        Assert.IsTrue(json.IsValid(Data.schemas[RequestType.Auth]));

        //check content
        var jsonDocument = JsonDocument.Parse(jsonData);
        Assert.IsTrue(jsonDocument.RootElement.TryGetProperty("token", out var token));

        //set token for next tests
        Data.SetToken(token.GetString());
    }


    [TestMethod]
    [DynamicData(nameof(Data.GetInvalidAuthCases), typeof(Data), DynamicDataSourceType.Method)]
    public async Task InvalidAuth(int expectedcode, Dictionary<string, object> authData, string name)
    {
        //create and send request
        var authRequest = new AuthRequest(_baseRequest);
        var response = await authRequest.Send(authData);

        //check status code
        Assert.IsTrue(response.Status == expectedcode);
    }

    [TestCleanup]
    public async Task TearDownAPITesting()
    {
        await _baseRequest.DisposeAsync();
    }
}