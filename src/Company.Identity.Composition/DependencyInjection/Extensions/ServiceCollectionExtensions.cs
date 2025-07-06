using Company.Identity.Application.Auth.Extensions;
using Company.Identity.Domain.User.Extensions;
using Company.Identity.Infrastructure.Auth.Extensions;
using Company.Identity.Infrastructure.Email.Extensions;
using Company.Identity.Persistence.IdentityDb.Extensions;
using Company.Identity.Shared.System.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Composition.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddUserApplication();
        services.AddUserDomain();
        services.AddAuthInfrastructure(configuration);
        services.AddEmailInfrastructure();
        services.AddIdentityPersistence(configuration);
        services.AddTestingFacades();
        return services;
    }
}