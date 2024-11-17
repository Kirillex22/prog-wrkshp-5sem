using Microsoft.Playwright;

namespace RestfulBookerTests;

public class DeleteBookingRequest : BaseRequest
{
    public DeleteBookingRequest(IAPIRequestContext baseRequest) : base(baseRequest) { }
    public override Task<IAPIResponse> Send(IDictionary<string, object> data)
    {
        var id = data["id"];
        return _baseRequest.DeleteAsync($"/booking/{id}");
    }
}