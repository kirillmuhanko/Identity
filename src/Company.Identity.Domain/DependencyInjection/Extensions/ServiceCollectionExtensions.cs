using Company.Identity.Domain.User.Interfaces.Services;
using Company.Identity.Domain.User.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Domain.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}