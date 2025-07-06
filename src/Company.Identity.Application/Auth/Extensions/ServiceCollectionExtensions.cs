using Company.Identity.Application.Auth.Handlers;
using Company.Identity.Application.Auth.Interfaces.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Application.Auth.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        // Handlers
        services.AddScoped<ICreateUserHandler, CreateUserHandler>();

        return services;
    }
}