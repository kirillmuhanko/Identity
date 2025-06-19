using Company.Identity.Domain.User.Interfaces.Repositories;
using Company.Identity.Persistence.Contexts;
using Company.Identity.Persistence.Repositories;
using Company.Identity.Persistence.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
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