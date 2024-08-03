using KanzApi.Common.Exceptions;

namespace KanzApi.Common.Middlewares;

public class UrwayAuthorizerMiddleware(RequestDelegate next, IConfiguration configuration)
{

    private readonly RequestDelegate _next = next;

    private readonly IConfiguration _configuration = configuration;

    public async Task Invoke(HttpContext context)
    {
        HttpRequest request = context.Request;

        string path = _configuration.GetValue<string>("Urway:CallbackPath")!;
        if (path.Equals(request.Path))
        {
            string authKey = _configuration.GetValue<string>("Urway:ApiAuthKey")!;
            string? value = request.Headers[authKey];

            string authValue = _configuration.GetValue<string>("Urway:ApiAuthValue")!;
            if (!authValue.Equals(value))
            {
                throw new UnauthorizedException(path);
            }
        }

        await _next(context);
    }
}
