using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Shared.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        return services;
    }
}