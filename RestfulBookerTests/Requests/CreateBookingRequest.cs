using Microsoft.Playwright;

namespace RestfulBookerTests;

public class CreateBookingRequest : BaseRequest
{
    public CreateBookingRequest(IAPIRequestContext baseRequest) : base(baseRequest) { }
    public override Task<IAPIResponse> Send(IDictionary<string, object> data)
    {
        return _baseRequest.PostAsync(
            "/booking", new()
            {
                DataObject = data
            }
        );
    }
}