using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Domain.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}