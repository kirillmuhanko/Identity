using Company.Identity.Application.User.Handlers;
using Company.Identity.Application.User.Interfaces.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Application.User.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserApplication(this IServiceCollection services)
    {
        // Handlers
        services.AddScoped<ICreateUserHandler, CreateUserHandler>();

        return services;
    }
}