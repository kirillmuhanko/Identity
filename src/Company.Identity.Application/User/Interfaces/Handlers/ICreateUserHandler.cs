using Company.Identity.Application.User.Commands;

namespace Company.Identity.Application.User.Interfaces.Handlers;

public interface ICreateUserHandler
{
    Task HandleAsync(CreateUserCommand command);
}