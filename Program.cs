using KanzApi.Configurations;

namespace KanzApi;

public class Program
{

    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        ConfigurationManager config = builder.Configuration;
        config.AddUserSecrets<Program>();

        builder
        .ConfigureLogging()
        .ConfigureRepository()
        .ConfigureService()
        .ConfigureController()
        .ConfigureMapping()
        .ConfigureLocale()
        .ConfigureSwagger()
        .ConfigureAuth()
        .ConfigureVersioning()
        .ConfigureHttpClient();

        builder.Build()
        .ConfigureMiddleware()
        .ConfigureLocale()
        .ConfigureSwagger()
        .ConfigureAuth()
        .ConfigureController()
        .Run();
    }
}
