using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

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
            ExtraHTTPHeaders = Data.deleteHeaders
        });
    }

    [TestMethod]
    public async Task SuccesfullCreate()
    {
        //create and send request
        var createBookingRqst = new CreateBookingRequest(_baseRequest);

        var response = await createBookingRqst.Send(Data.booking);
        var jsonData = await response.TextAsync();
        var receivedJson = JsonDocument.Parse(jsonData);

        //check status code
        await Expect(response).ToBeOKAsync();

        //schema validating
        var json = JObject.Parse(jsonData);
        Assert.IsTrue(json.IsValid(Data.schemas["Create"]));

        //check content
        Assert.IsTrue(receivedJson.RootElement.TryGetProperty("bookingid", out var bookingId));
        Assert.IsTrue(receivedJson.RootElement.TryGetProperty("booking", out var booking));
        var receivedJsonString = booking.ToString();
        var sendedJsonString = JsonSerializer.Serialize(Data.booking);
        Assert.AreEqual(receivedJsonString, sendedJsonString);

        //remember id of created entity 
        _dtd["id"] = bookingId;
    }

    [TestCleanup]
    public async Task TearDownAPITesting()
    {
        //delete created entity
        var deleteBookingRqst = new DeleteBookingRequest(_baseRequest);
        var response = await deleteBookingRqst.Send(_dtd);
        await Expect(response).ToBeOKAsync();

        await _baseRequest.DisposeAsync();
    }
}