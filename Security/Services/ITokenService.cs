using KanzApi.Security.Models;

namespace KanzApi.Security.Services;

public interface ITokenService
{

    TokenResponse Generate(TokenRequest request);
}
