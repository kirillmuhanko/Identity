namespace Company.Identity.Application.Event.Interfaces.Dispatchers;

public interface IEventDispatcher
{
    Task DispatchAsync<TEvent>(TEvent eventToDispatch)  where TEvent : class;
}