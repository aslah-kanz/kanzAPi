using KanzApi.Messaging.Services;
using KanzApi.Vendors.TinyUrl.Models;

namespace KanzApi.Vendors.TinyUrl.Services;

public abstract class TinyUrlService<T>(IHttpClientFactory httpClientFactory, ILogger<TinyUrlService<T>> logger)
: HttpClientService<TinyUrlResponse<T>>(httpClientFactory, logger)
{

    protected override void Error(HttpResponseMessage httpResponse)
    {
        throw new TinyUrlUnknownException();
    }

    protected override void Error(TinyUrlResponse<T> response)
    {
        throw new TinyUrlErrorException(response.Code, String.Join(", ", response.Errors) ?? "?");
    }
}
