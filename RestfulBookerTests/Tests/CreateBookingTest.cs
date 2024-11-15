using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Text.Json;

namespace RestfulBookerTests;

[TestClass]
public class CreateBookingTest : PlaywrightTest
{
    private IAPIRequestContext _baseRequest = null!;
    private Dictionary<string, object> _dtd = new Dictionary<string, object>() { { "id", "" } };

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
            ExtraHTTPHeaders = new Dictionary<string, string>(){
                {"Content-Type", "application/json"},
                {"Cookie", $"token={Data.token}"}
            }
        });
    }

    [TestMethod]
    public async Task SuccesfullCreate()
    {
        var dBRequest = new DeleteBookingRequest(_baseRequest);
        var cBRequest = new CreateBookingRequest(_baseRequest);
        var gBRequest = new GetBookingRequest(_baseRequest);

        var response = await cBRequest.Send(Data.booking);
        var receivedJson = JsonDocument.Parse(await response.TextAsync());
        receivedJson.RootElement.TryGetProperty("bookingid", out var bookingId);

        await Expect(response).ToBeOKAsync();

        _dtd["id"] = bookingId;
        response = await dBRequest.Send(_dtd);
        await Expect(response).ToBeOKAsync();

        response = await gBRequest.Send(_dtd);
        await Expect(response).Not.ToBeOKAsync();
    }

    [TestCleanup]
    public async Task TearDownAPITesting()
    {
        await _baseRequest.DisposeAsync();
    }
}