using KanzApi.Messaging.Services;
using KanzApi.Vendors.Msegat.Models;

namespace KanzApi.Vendors.Msegat.Services;

public abstract class MsegatService(IHttpClientFactory httpClientFactory, ILogger<MsegatService> logger)
: HttpClientService<MsegatResponse>(httpClientFactory, logger)
{

    protected override void Error(HttpResponseMessage httpResponse)
    {
        throw new MsegatUnknownException();
    }

    protected override void Error(MsegatResponse response)
    {
        string? code = response.Code?.ToString();
        throw new MsegatErrorException(code ?? "?", response.Message ?? "?")
        {
            DataObject = new MsegatErrorResponse()
            {
                Code = code,
                Message = response.Message
            }
        };
    }
}
