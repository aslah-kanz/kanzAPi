using KanzApi.Extensions;
using KanzApi.Utils;

namespace KanzApi.Common.Services;

public class SessionService(IHttpContextAccessor context) : ISessionService
{

    private readonly IHttpContextAccessor _context = context;

    public int? CurrentPrincipalId()
    {
        return _context.HttpContext?.User.PrincipalId();
    }

    public int CurrentAuditorId()
    {
        return CurrentPrincipalId() ?? Constants.SystemPrincipalId;
    }

    public string? CurrentTokenId()
    {
        return _context.HttpContext?.User.TokenId();
    }
}
