using Company.Identity.Domain.User.Interfaces.Services;
using Company.Identity.Domain.User.Interfaces.Specifications;
using Company.Identity.Domain.User.Services;
using Company.Identity.Domain.User.Specifications;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Domain.User.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserDomain(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IUserService, UserService>();

        // Specifications
        services.AddSingleton<IUserSpecification, UserSpecification>();
        return services;
    }
}