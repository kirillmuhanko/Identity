using Company.Identity.Application.User.Handlers;
using Company.Identity.Application.User.Interfaces.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Handlers
        services.AddScoped<ICreateUserHandler, CreateUserHandler>();

        return services;
    }
}