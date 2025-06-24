using Company.Identity.Application.Event.Interfaces.Dispatchers;
using Company.Identity.Application.User.Commands;
using Company.Identity.Application.User.Events;
using Company.Identity.Application.User.Interfaces.Handlers;
using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Services;
using Company.Identity.Shared.Result.Models;

namespace Company.Identity.Application.User.Handlers;

public class CreateUserHandler(IUserService userService, IEventDispatcher dispatcher) : ICreateUserHandler
{
    public async Task<ResultModel<bool>> HandleAsync(CreateUserCommand command)
    {
        var user = new UserEntity(command.UserName, command.Email);
        var result = await userService.AddUserAsync(user);

        if (!result.Success)
        {
            return ResultModel<bool>.Fail(result.Errors);
        }

        var userCreatedEvent = new UserCreatedEvent(result.Value.Email, result.Value.UserName);
        await dispatcher.DispatchAsync(userCreatedEvent);

        return ResultModel<bool>.Ok(true);
    }
}