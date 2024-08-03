using KanzApi.Messaging.Models;
using KanzApi.Messaging.Services;

namespace KanzApi.Vendors.SendGrid.Services;

public abstract class SendGridService(IHttpClientFactory httpClientFactory, ILogger<SendGridService> logger)
: HttpClientService<VoidResponse>(httpClientFactory, logger, true)
{

    protected override void Error(HttpResponseMessage httpResponse)
    {
        throw new SendGridErrorException((int)httpResponse.StatusCode, httpResponse.StatusCode.ToString());
    }
}
