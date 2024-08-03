using KanzApi.Utils;

namespace KanzApi.Configurations;

public static class HttpClientConfig
{

    public static WebApplicationBuilder ConfigureHttpClient(this WebApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;
        ConfigurationManager config = builder.Configuration;

        services.AddHttpClient(Constants.GraphLoginClient, client =>
        {
            client.BaseAddress = new Uri(config.GetValue<string>("Graph:Login:BaseUrl")!);
        });
        services.AddHttpClient(Constants.GraphMailClient, client =>
        {
            client.BaseAddress = new Uri(config.GetValue<string>("Graph:Mail:BaseUrl")!);
        });
        services.AddHttpClient(Constants.MsegatClient, client =>
        {
            client.BaseAddress = new Uri(config.GetValue<string>("Msegat:BaseUrl")!);
        });
        services.AddHttpClient(Constants.OtoClient, client =>
        {
            client.BaseAddress = new Uri(config.GetValue<string>("Oto:BaseUrl")!);
        });
        services.AddHttpClient(Constants.SendGridClient, client =>
        {
            client.BaseAddress = new Uri(config.GetValue<string>("SendGrid:BaseUrl")!);
        });
        services.AddHttpClient(Constants.TinyUrlClient, client =>
        {
            client.BaseAddress = new Uri(config.GetValue<string>("TinyUrl:BaseUrl")!);
        });
        services.AddHttpClient(Constants.UrwayClient, client =>
        {
            client.BaseAddress = new Uri(config.GetValue<string>("Urway:BaseUrl")!);
        });

        return builder;
    }
}
