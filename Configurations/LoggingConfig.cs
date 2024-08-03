using Serilog;
using Serilog.Core;

namespace KanzApi.Configurations;

public static class LoggingConfig
{

    public static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder)
    {
        Logger logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

        builder.Logging
        .ClearProviders()
        .AddSerilog(logger);

        return builder;
    }
}
