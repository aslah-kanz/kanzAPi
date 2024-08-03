using KanzApi.Transaction.Models;
using KanzApi.Utils;
using KanzApi.Vendors.Oto.Models;
using System.Net.Http.Headers;

namespace KanzApi.Vendors.Oto.Services;

public class OtoCheckoutService(IHttpClientFactory httpClientFactory,
ILogger<OtoCheckoutService> logger, IOtoAuthorizationService otoAuthorizationService)
: OtoService(httpClientFactory, logger), IOtoCheckoutService
{

    private readonly IOtoAuthorizationService _otoAuthorizationService = otoAuthorizationService;

    public OtoDeliveryFeeResponse CheckOtoDeliveryFee(OtoDeliveryFeeRequest request)
    {
        using HttpClient client = _httpClientFactory.CreateClient(Constants.OtoClient);

        OtoRefreshTokenResponse token = _otoAuthorizationService.RefreshToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        return Post<OtoDeliveryFeeRequest, OtoDeliveryFeeResponse>(
            client, "/rest/v2/checkOTODeliveryFee", request)!;
    }

    public OtoDeliveryFeeResponse CheckOtoDeliveryFee(DeliveryDetail deliveryDetail)
    {
        double side = Math.Pow((double)deliveryDetail.Volume!, 1.0 / 3);
        OtoDeliveryFeeRequest request = new()
        {
            OriginCity = deliveryDetail.OriginCity,
            DestinationCity = deliveryDetail.DestinationCity,
            OriginCountry = deliveryDetail.OriginCountry,
            DestinationCountry = deliveryDetail.DestinationCountry,
            Weight = deliveryDetail.Weight,
            Currency = "SAR",
            Length = side,
            Width = side,
            Height = side
        };

        return CheckOtoDeliveryFee(request);
    }
}
