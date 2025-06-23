using Company.Identity.Application.User.Commands;
using Company.Identity.Application.User.Interfaces.Handlers;
using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Services;

namespace Company.Identity.Application.User.Handlers;

public class CreateUserHandler(IUserService userService) : ICreateUserHandler
{
    public async Task HandleAsync(CreateUserCommand command)
    {
        var user = new UserEntity(command.UserName, command.Email);
        await userService.AddUserAsync(user);
    }
}