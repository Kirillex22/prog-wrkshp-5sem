using Microsoft.Playwright;

namespace RestfulBookerTests;

public class AuthRequest : BaseRequest
{
    public AuthRequest(IAPIRequestContext baseRequest) : base(baseRequest) { }
    public override Task<IAPIResponse> Send(IDictionary<string, object> data)
    {
        return _baseRequest.PostAsync(
            "/auth", new()
            {
                DataObject = data
            }
        );
    }
}