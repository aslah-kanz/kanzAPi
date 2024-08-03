using Asp.Versioning;

namespace KanzApi.Configurations;

public static class VersioningConfig
{

    public static WebApplicationBuilder ConfigureVersioning(this WebApplicationBuilder builder)
    {
        builder.Services
        .AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return builder;
    }
}
