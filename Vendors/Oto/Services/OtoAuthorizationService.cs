using KanzApi.Utils;
using KanzApi.Vendors.Oto.Models;

namespace KanzApi.Vendors.Oto.Services;

public class OtoAuthorizationService(IHttpClientFactory httpClientFactory,
ILogger<OtoAuthorizationService> logger, IConfiguration configuration)
: OtoService(httpClientFactory, logger), IOtoAuthorizationService
{

    private readonly IConfiguration _configuration = configuration;

    public OtoRefreshTokenResponse RefreshToken()
    {
        OtoRefreshTokenRequest request = new()
        {
            RefreshToken = _configuration.GetValue<string>("Oto:RefreshToken")!
        };
        return Post<OtoRefreshTokenRequest, OtoRefreshTokenResponse>(
            Constants.OtoClient, "/rest/v2/refreshToken", request)!;
    }
}
