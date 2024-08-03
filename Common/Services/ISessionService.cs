namespace KanzApi.Common.Services;

public interface ISessionService
{

    int? CurrentPrincipalId();

    int CurrentAuditorId();

    string? CurrentTokenId();
}
