using KanzApi.Messaging.Services;
using KanzApi.Vendors.Urway.Models;

namespace KanzApi.Vendors.Urway.Services;

public abstract class UrwayService(IHttpClientFactory httpClientFactory, ILogger<UrwayService> logger)
: HttpClientService<UrwayResponse>(httpClientFactory, logger)
{

    protected override void Error(HttpResponseMessage httpResponse)
    {
        throw new UrwayUnknownException();
    }

    protected override void Error(UrwayResponse response)
    {
        string? code = response.ResponseCode;
        throw new UrwayErrorException(code!, response.Result ?? "?")
        {
            DataObject = new
            {
                Code = code,
                response.Result
            }
        };
    }
}
