using KanzApi.Messaging.Services;
using KanzApi.Utils;
using KanzApi.Vendors.Oto.Models;
using System.Net.Http.Headers;

namespace KanzApi.Vendors.Oto.Services;

public class OtoAwbPrinterService(IHttpClientFactory httpClientFactory,
ILogger<OtoAwbPrinterService> logger, IOtoAuthorizationService otoAuthorizationService)
: HttpClientService<OtoAwbPrinterResponse>(httpClientFactory, logger), IOtoAwbPrinterService
{

    private readonly IOtoAuthorizationService _otoAuthorizationService = otoAuthorizationService;

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

    public OtoAwbPrinterResponse Print(string id)
    {
        using HttpClient client = _httpClientFactory.CreateClient(Constants.OtoClient);

        OtoRefreshTokenResponse token = _otoAuthorizationService.RefreshToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        return Get<OtoAwbPrinterResponse>(client, "/rest/v2/print/" + id)!;
    }
}
