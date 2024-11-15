using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Text.Json;

namespace RestfulBookerTests;

[TestClass]
public class DeleteBookingTest : PlaywrightTest
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
    public async Task SuccesfullDelete()
    {
        var dBRequest = new DeleteBookingRequest(_baseRequest);
        var cBRequest = new CreateBookingRequest(_baseRequest);

        var response = await cBRequest.Send(Data.booking);
        var receivedJson = JsonDocument.Parse(await response.TextAsync());

        await Expect(response).ToBeOKAsync();
        Assert.IsTrue(receivedJson.RootElement.TryGetProperty("bookingid", out var bookingId));
        Assert.IsTrue(receivedJson.RootElement.TryGetProperty("booking", out var booking));

        var receivedJsonString = booking.ToString();
        var sendedJsonString = JsonSerializer.Serialize(Data.booking);

        Assert.AreEqual(receivedJsonString, sendedJsonString);

        _dtd["id"] = bookingId;
        response = await dBRequest.Send(_dtd);
        await Expect(response).ToBeOKAsync();
    }

    [TestCleanup]
    public async Task TearDownAPITesting()
    {
        await _baseRequest.DisposeAsync();
    }
}