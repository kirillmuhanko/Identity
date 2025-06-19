using Company.Identity.Application.Extensions;
using Company.Identity.Domain.Extensions;
using Company.Identity.Infrastructure.Extensions;
using Company.Identity.Persistence.Extensions;
using Company.Identity.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Composition.Extensions;

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