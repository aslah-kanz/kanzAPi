using KanzApi.Common.Middlewares;

namespace KanzApi.Configurations;

public static class MiddlewareConfig
{

    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        app.UseMiddleware<HttpErrorHandlerMiddleware>();
        app.UseMiddleware<HttpRequestLoggerMiddleware>();
        app.UseMiddleware<UrwayAuthorizerMiddleware>();

        return app;
    }
}
