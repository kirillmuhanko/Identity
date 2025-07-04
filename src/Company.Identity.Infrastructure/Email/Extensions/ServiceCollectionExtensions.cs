using Company.Identity.Application.Event.Interfaces.Dispatchers;
using Company.Identity.Application.Event.Interfaces.Handlers;
using Company.Identity.Application.User.Events;
using Company.Identity.Infrastructure.Email.Handlers;
using Company.Identity.Infrastructure.Email.Interfaces.Senders;
using Company.Identity.Infrastructure.Email.Senders;
using Company.Identity.Infrastructure.Event.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Infrastructure.Email.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmailInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IEventDispatcher, InMemoryEventDispatcher>();
        services.AddScoped<IEmailSender, ConsoleEmailSender>();
        services.AddScoped<IEventHandler<UserCreatedEvent>, SendWelcomeEmailHandler>();
        return services;
    }
}