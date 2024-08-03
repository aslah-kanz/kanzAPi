using Microsoft.AspNetCore.Localization;

namespace KanzApi.Configurations;

public static class LocaleConfig
{

    public static WebApplicationBuilder ConfigureLocale(this WebApplicationBuilder builder)
    {
        builder.Services
        .AddLocalization()
        .AddRequestLocalization(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("en");
            options.ApplyCurrentCultureToResponseHeaders = true;
            options.SupportedCultures = options.SupportedUICultures = [
                new("ar"),
                new("en")
            ];
        });

        return builder;
    }

    public static WebApplication ConfigureLocale(this WebApplication app)
    {
        app.UseRequestLocalization();

        return app;
    }
}
