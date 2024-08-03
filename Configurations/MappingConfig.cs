using Mapster;
using MapsterMapper;
using System.Reflection;

namespace KanzApi.Configurations;

public static class MappingConfig
{

    public static WebApplicationBuilder ConfigureMapping(this WebApplicationBuilder builder)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        builder.Services
        .AddSingleton(config)
        .AddScoped<IMapper, ServiceMapper>();

        return builder;
    }
}
