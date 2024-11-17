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
            ExtraHTTPHeaders = Data.deleteHeaders
        });
    }

    [TestMethod]
    public async Task SuccesfullDelete()
    {
        //create and send request
        var deleteBookingRqst = new DeleteBookingRequest(_baseRequest);
        var createBookingRqst = new CreateBookingRequest(_baseRequest);
        var getBookingRqst = new GetBookingRequest(_baseRequest);

        var response = await createBookingRqst.Send(Data.booking);
        var receivedJson = JsonDocument.Parse(await response.TextAsync());
        receivedJson.RootElement.TryGetProperty("bookingid", out var bookingId);

        //check status code
        await Expect(response).ToBeOKAsync();

        //remember id of created entit
        _dtd["id"] = bookingId;

        response = await deleteBookingRqst.Send(_dtd);
        await Expect(response).ToBeOKAsync();

        response = await getBookingRqst.Send(_dtd);
        await Expect(response).Not.ToBeOKAsync();
    }

    [TestCleanup]
    public async Task TearDownAPITesting()
    {
        await _baseRequest.DisposeAsync();
    }
}