using AutoMapper;
using Company.Identity.Application.Event.Interfaces.Dispatchers;
using Company.Identity.Application.User.Commands;
using Company.Identity.Application.User.DTOs;
using Company.Identity.Application.User.Events;
using Company.Identity.Application.User.Interfaces.Handlers;
using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Services;
using Company.Identity.Shared.Result.Models;

namespace Company.Identity.Application.User.Handlers;

public class CreateUserHandler(
    IEventDispatcher dispatcher,
    IMapper mapper,
    IUserService userService) : ICreateUserHandler
{
    public async Task<ResultModel<CreateUserDto>> HandleAsync(CreateUserCommand command)
    {
        var user = new UserEntity(command.UserName, command.Email);
        var result = await userService.AddUserAsync(user);

        if (!result.Success) return ResultModel<CreateUserDto>.Fail(result.Errors);

        var userCreatedEvent = new UserCreatedEvent(result.Value.Email, result.Value.UserName);
        await dispatcher.DispatchAsync(userCreatedEvent);
        var createUserDto = mapper.Map<CreateUserDto>(result.Value);
        return ResultModel<CreateUserDto>.Ok(createUserDto);
    }
}