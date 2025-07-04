using Company.Identity.Shared.System.Facades;
using Company.Identity.Shared.System.Interfaces.Facades;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Shared.System.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTestingFacades(this IServiceCollection services)
    {
        // Register system facades (e.g., DateTime) to abstract external dependencies and improve testability
        services.AddScoped<IDateTimeFacade, DateTimeFacade>();
        return services;
    }
}