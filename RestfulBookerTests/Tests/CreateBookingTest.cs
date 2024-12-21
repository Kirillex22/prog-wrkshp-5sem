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
    [DynamicData(nameof(Data.GetValidCreateCases), typeof(Data), DynamicDataSourceType.Method)]
    public async Task ValidCreate(int expectedcode, Dictionary<string, object> createData, string name)
    {
        //create and send request
        var createBookingRqst = new CreateBookingRequest(_baseRequest);

        var response = await createBookingRqst.Send(createData);
        var jsonData = await response.TextAsync();
        var receivedJson = JsonDocument.Parse(jsonData);

        //check status code
        Assert.IsTrue(response.Status == expectedcode);

        //schema validating
        var json = JObject.Parse(jsonData);
        Assert.IsTrue(json.IsValid(Data.schemas[RequestType.Create]));

        Assert.IsTrue(receivedJson.RootElement.TryGetProperty("bookingid", out var bookingId));

        //remember id of created entity 
        _dtd["id"] = bookingId;
    }


    [TestMethod]
    [DynamicData(nameof(Data.GetInvalidCreateCases), typeof(Data), DynamicDataSourceType.Method)]
    public async Task InvalidCreate(int expectedcode, Dictionary<string, object> createData, string name)
    {
        //create and send request
        var createBookingRqst = new CreateBookingRequest(_baseRequest);

        var response = await createBookingRqst.Send(createData);
        var jsonData = await response.TextAsync();

        //check status code
        Assert.IsTrue(response.Status == expectedcode);

        JsonDocument.Parse(jsonData).RootElement.TryGetProperty("bookingid", out var bookingId);

        //remember id of created entity 
        _dtd["id"] = bookingId;
    }


    [TestCleanup]
    public async Task DeleteEntityAfterCreating()
    {
        var deleteBookingRqst = new DeleteBookingRequest(_baseRequest);
        await deleteBookingRqst.Send(_dtd);
        await _baseRequest.DisposeAsync();
    }
}