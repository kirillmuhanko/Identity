using Company.Identity.Application.DependencyInjection.Extensions;
using Company.Identity.Domain.DependencyInjection.Extensions;
using Company.Identity.Infrastructure.DependencyInjection.Extensions;
using Company.Identity.Persistence.DependencyInjection.Extensions;
using Company.Identity.Shared.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Composition.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddDomain();
        services.AddInfrastructure();
        services.AddPersistence(configuration);
        services.AddShared();
        return services;
    }
}