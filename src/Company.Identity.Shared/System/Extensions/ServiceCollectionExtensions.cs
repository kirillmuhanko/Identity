using Company.Identity.Shared.System.Facades;
using Company.Identity.Shared.System.Interfaces.Facades;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Shared.System.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTestingFacades(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeFacade, DateTimeFacade>();
        return services;
    }
}