using Microsoft.Playwright;

namespace RestfulBookerTests;

public class GetBookingRequest : BaseRequest
{
    public GetBookingRequest(IAPIRequestContext baseRequest) : base(baseRequest) { }
    public override Task<IAPIResponse> Send(IDictionary<string, object> data)
    {
        var id = data["id"];
        return _baseRequest.GetAsync($"/booking/{id}", new());
    }
}