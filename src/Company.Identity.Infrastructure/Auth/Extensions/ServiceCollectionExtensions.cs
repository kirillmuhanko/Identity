using Company.Identity.Application.Auth.Services;
using Company.Identity.Infrastructure.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Infrastructure.Auth.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}