using System.Net;
using AutoMapper;
using Company.Identity.Application.Auth.Commands;
using Company.Identity.Application.Auth.DTOs;
using Company.Identity.Application.Auth.Events;
using Company.Identity.Application.Auth.Interfaces.Handlers;
using Company.Identity.Application.Auth.Interfaces.Services;
using Company.Identity.Application.Event.Interfaces.Dispatchers;
using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Repositories;
using Company.Identity.Domain.User.Interfaces.Specifications;
using Company.Identity.Shared.Result.Models;

namespace Company.Identity.Application.Auth.Handlers;

public class CreateUserHandler(
    IAuthService authService,
    IEventDispatcher dispatcher,
    IMapper mapper,
    IUserSpecification userSpecification,
    IUserRepository userRepository) : ICreateUserHandler
{
    public async Task<ResultModel<CreateUserDto>> HandleAsync(CreateUserCommand command)
    {
        var hasEmailAndUserNameSpec = userSpecification.HasUserNameAndEmail(command.UserName, command.Email);
        var hasEmailAndUserNameResult = await userRepository.AnyAsync(hasEmailAndUserNameSpec);

        if (hasEmailAndUserNameResult.Value)
            return ResultModel<CreateUserDto>.Fail(
                "A user with the same email and username already exists.",
                HttpStatusCode.Conflict
            );

        var passwordHash = authService.HashPassword(command.Password);
        var userEntity = new UserEntity(command.UserName, command.Email, passwordHash);
        var result = await userRepository.AddAsync(userEntity);

        if (!result.IsSuccess)
            return ResultModel<CreateUserDto>.FailFrom(result);

        var userCreatedEvent = new UserCreatedEvent(result.Value.Email, result.Value.UserName);
        await dispatcher.DispatchAsync(userCreatedEvent);
        var createUserDto = mapper.Map<CreateUserDto>(result.Value);
        return ResultModel<CreateUserDto>.Ok(createUserDto);
    }
}