using KanzApi.Common.Filters;
using KanzApi.Common.Models.Binding;
using KanzApi.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KanzApi.Configurations;

public static class ControllerConfig
{

    public static WebApplicationBuilder ConfigureController(this WebApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;

        services
        .AddControllers(options =>
        {
            options.Filters.Add<HttpResponseExceptionFilter>();
            options.Filters.Add(new ProducesAttribute("application/json"));
            options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
            options.ModelBinderProviders.Insert(0, new UrlDecoderModelBinderProvider());
        })
        .AddDataAnnotationsLocalization(o =>
        {
            o.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(ErrorMessages));
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });

        services
        .Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        })
        .AddRouting(
            options => options.LowercaseUrls = true);

        return builder;
    }

    public static WebApplication ConfigureController(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}
