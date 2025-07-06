using Company.Identity.Application.Auth.Interfaces.Services;
using Company.Identity.Infrastructure.Auth.Options;
using Company.Identity.Infrastructure.Auth.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Infrastructure.Auth.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Options
        services
            .AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // Services
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}