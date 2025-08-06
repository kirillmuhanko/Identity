using Company.Identity.Domain.User.Interfaces.Specifications;
using Company.Identity.Domain.User.Interfaces.Validators;
using Company.Identity.Domain.User.Specifications;
using Company.Identity.Domain.User.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Domain.User.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserDomain(this IServiceCollection services)
    {
        // Specifications
        services.AddSingleton<IUserSpecification, UserSpecification>();

        // Validators
        services.AddSingleton<IEmailValidator, EmailValidator>();
        services.AddSingleton<IPasswordValidator, PasswordValidator>();
        return services;
    }
}