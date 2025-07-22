using Company.Identity.Application.Event.Interfaces.Dispatchers;
using Company.Identity.Application.Event.Interfaces.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Company.Identity.Infrastructure.Event.Dispatchers;

public class InMemoryEventDispatcher(
    IServiceProvider serviceProvider, 
    ILogger<InMemoryEventDispatcher> logger)
    : IEventDispatcher
{
    public async Task DispatchAsync<TEvent>(TEvent eventToDispatch) where TEvent : class
    {
        var handlers = serviceProvider.GetServices<IEventHandler<TEvent>>();

        try
        {
            foreach (var handler in handlers) 
                await handler.HandleAsync(eventToDispatch);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while dispatching event of type {EventType}.", typeof(TEvent).Name);
        }
    }
}