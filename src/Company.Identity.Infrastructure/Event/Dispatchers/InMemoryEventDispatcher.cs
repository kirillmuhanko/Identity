using Company.Identity.Application.Event.Interfaces.Dispatchers;
using Company.Identity.Application.Event.Interfaces.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Identity.Infrastructure.Event.Dispatchers;

public class InMemoryEventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    public async Task DispatchAsync<TEvent>(TEvent @event) where TEvent : class
    {
        var handlers = serviceProvider.GetServices<IEventHandler<TEvent>>();

        foreach (var handler in handlers)
            await handler.HandleAsync(@event);
    }
}