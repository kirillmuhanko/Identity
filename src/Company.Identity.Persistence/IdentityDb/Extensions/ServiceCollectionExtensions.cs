using Company.Identity.Domain.User.Interfaces.Repositories;
using Company.Identity.Persistence.IdentityDb.Contexts;
using Company.Identity.Persistence.IdentityDb.Repositories;
using Company.Identity.Persistence.IdentityDb.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Persistence.IdentityDb.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Validators
        services.AddValidatorsFromAssemblyContaining<UserEntityValidator>();
        return services;
    }
}