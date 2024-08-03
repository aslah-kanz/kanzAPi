using KanzApi.Vendors.Oto.Models;

namespace KanzApi.Vendors.Oto.Services;

public interface IOtoAuthorizationService
{

    OtoRefreshTokenResponse RefreshToken();
}
