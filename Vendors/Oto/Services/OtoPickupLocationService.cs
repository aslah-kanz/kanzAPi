using KanzApi.Account.Entities;
using KanzApi.Utils;
using KanzApi.Vendors.Oto.Models;
using System.Net.Http.Headers;

namespace KanzApi.Vendors.Oto.Services;

public class OtoPickupLocationService(IHttpClientFactory httpClientFactory,
ILogger<OtoPickupLocationService> logger, IOtoAuthorizationService otoAuthorizationService)
: OtoService(httpClientFactory, logger), IOtoPickupLocationService
{

    private readonly IOtoAuthorizationService _otoAuthorizationService = otoAuthorizationService;

    public OtoPickupLocationResponse Create(OtoPickupLocationRequest request)
    {
        using HttpClient client = _httpClientFactory.CreateClient(Constants.OtoClient);

        OtoRefreshTokenResponse token = _otoAuthorizationService.RefreshToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        return Post<OtoPickupLocationRequest, OtoPickupLocationResponse>(
            client, "/rest/v2/createPickupLocation", request)!;
    }

    public OtoPickupLocationResponse Create(Store store)
    {
        return Create(OtoPickupLocationRequest.From(store));
    }

    public OtoPickupLocationResponse Update(OtoPickupLocationRequest request)
    {
        using HttpClient client = _httpClientFactory.CreateClient(Constants.OtoClient);

        OtoRefreshTokenResponse token = _otoAuthorizationService.RefreshToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        return Post<OtoPickupLocationRequest, OtoPickupLocationResponse>(
            client, "/rest/v2/updatePickupLocation", request)!;
    }

    public OtoPickupLocationResponse Update(Store store)
    {
        return Update(OtoPickupLocationRequest.From(store));
    }
}
