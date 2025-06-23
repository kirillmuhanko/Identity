namespace Company.Identity.Application.Event.Interfaces.Handlers;

public interface IEventHandler<in T>
{
    Task HandleAsync(T @event);
}