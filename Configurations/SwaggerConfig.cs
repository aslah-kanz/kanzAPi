using Asp.Versioning.ApiExplorer;
using KanzApi.Configurations.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KanzApi.Configurations;

public static class SwaggerConfig
{

    public static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services
        .AddTransient<IConfigureOptions<SwaggerGenOptions>, VersionConfigureOptions>()
        .AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.DescribeAllParametersInCamelCase();
            options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Name = HeaderNames.Authorization,
                BearerFormat = "JWT",
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,
                Description = "Insert JWT token only without Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {{
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }
                },
                Array.Empty<string>()
            }});
            options.SchemaFilter<MaxSizeSchemaFilter>();
            options.SchemaFilter<MinSizeSchemaFilter>();
            options.SchemaFilter<NonNullableSchemaFilter>();
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            options.OperationFilter<DeprecatedOperationFilter>();
        });

        return builder;
    }

    public static WebApplication ConfigureSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.ShowCommonExtensions();
                foreach (ApiVersionDescription description in app.DescribeApiVersions())
                {
                    string url = $"/swagger/{description.GroupName}/swagger.json";
                    options.SwaggerEndpoint(url, description.GroupName);
                }
            });
        }

        return app;
    }
}
