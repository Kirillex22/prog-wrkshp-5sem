using Microsoft.Playwright;

namespace RestfulBookerTests;

public class BaseRequest
{
    protected IAPIRequestContext _baseRequest;
    public BaseRequest(IAPIRequestContext baseRequest)
    {
        _baseRequest = baseRequest;
    }

    public virtual Task<IAPIResponse> Send(IDictionary<string, object> data)
    {
        return _baseRequest.GetAsync("");
    }
}