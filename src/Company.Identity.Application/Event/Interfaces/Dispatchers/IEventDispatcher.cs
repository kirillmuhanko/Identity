namespace Company.Identity.Application.Event.Interfaces.Dispatchers;

public interface IEventDispatcher
{
    Task DispatchAsync<T>(T @event) where T : class;
}