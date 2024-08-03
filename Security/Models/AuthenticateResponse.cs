using KanzApi.Account.Models;

namespace KanzApi.Security.Models;

public class AuthenticateResponse
{

    public AuthenticatePrincipalResponse Principal { get; set; } = new();

    public TokenResponse AccessToken { get; set; } = new();

    public TokenResponse RefreshToken { get; set; } = new();

    [Obsolete]
    public string OtpCode { get; set; } = "";
}
