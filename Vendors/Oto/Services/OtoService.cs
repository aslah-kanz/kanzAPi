using KanzApi.Messaging.Services;
using KanzApi.Vendors.Oto.Models;

namespace KanzApi.Vendors.Oto.Services;

public abstract class OtoService(IHttpClientFactory httpClientFactory, ILogger<OtoService> logger)
: HttpClientService<OtoResponse>(httpClientFactory, logger)
{

    protected override void Error(HttpResponseMessage httpResponse)
    {
        OtoErrorWrapperResponse response = httpResponse.Content.ReadFromJsonAsync<OtoErrorWrapperResponse>().Result!;

        if (response.Code != null)
        {
            string? message = response.Message ?? response.ErrorMessage;
            throw new OtoErrorException(response.Code.ToString()!, message ?? "?")
            {
                DataObject = new OtoErrorResponse()
                {
                    Code = response.Code,
                    Message = message
                }
            };
        }
        else if (response.ErrorCode != null)
        {
            string? message = response.Message ?? response.ErrorMessage;
            throw new OtoErrorException(response.ErrorCode.ToString()!, message ?? "?")
            {
                DataObject = new OtoErrorResponse()
                {
                    Code = response.ErrorCode,
                    Message = message
                }
            };
        }
        else if (response.Error?.Code != null)
        {
            throw new OtoErrorException(response.Error?.Code.ToString()!, response.Error?.Message ?? "?")
            {
                DataObject = response.Error
            };
        }
        else
        {
            throw new OtoUnknownException();
        }
    }

    protected override void Error(OtoResponse response)
    {
        if (response.ErrorCode != null)
        {
            throw new OtoErrorException(response.ErrorCode.ToString() ?? "?", response.ErrorMessage ?? "?")
            {
                DataObject = new OtoErrorResponse()
                {
                    Code = response.ErrorCode ?? -1,
                    Message = response.ErrorMessage
                }
            };
        }
        else
        {
            throw new OtoUnknownException();
        }
    }
}
