using KanzApi.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace KanzApi.Configurations;

public static class AuthConfig
{

    private const string DefaultCorsPolicy = "DefaultCorsPolicy";

    private static AuthorizationOptions AddPolicy(this AuthorizationOptions options, string name)
    {
        options.AddPolicy(name, policy => policy.RequireClaim(Constants.PolicyClaimType, name));
        return options;
    }

    public static WebApplicationBuilder ConfigureAuth(this WebApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;
        ConfigurationManager config = builder.Configuration;

        services
        .AddCors(options =>
        {
            options.AddPolicy(
                name: DefaultCorsPolicy,
                policy =>
                policy.WithOrigins(config.GetSection("KanzApi:CorsPolicyOrigins").Get<string[]>()!)
                .AllowAnyHeader()
                .WithMethods("GET", "POST", "PUT", "DELETE"));
        })
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                ValidAudience = config.GetValue<string>("KanzApi:Jwt:Audience"),
                ValidIssuer = config.GetValue<string>("KanzApi:Jwt:Issuer"),
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(config.GetValue<string>("KanzApi:Jwt:SigningKey")!))
            };
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    return Task.FromException(context.Exception);
                }
            };
        });

        services.AddAuthorization(options =>
        {
            AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.FallbackPolicy = policy;

            foreach (string name in Privileges.ToSet()) options.AddPolicy(name);
        });

        return builder;
    }

    public static WebApplication ConfigureAuth(this WebApplication app)
    {
        app
        .UseHttpsRedirection()
        .UseRouting()
        .UseCors(DefaultCorsPolicy)
        .UseAuthentication()
        .UseAuthorization();

        return app;
    }
}
