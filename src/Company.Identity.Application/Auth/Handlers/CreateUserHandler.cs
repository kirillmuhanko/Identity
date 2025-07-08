using System.Net;
using Company.Identity.Application.Auth.Commands;
using Company.Identity.Application.Auth.DTOs;
using Company.Identity.Application.Auth.Events;
using Company.Identity.Application.Auth.Interfaces.Handlers;
using Company.Identity.Application.Auth.Interfaces.Services;
using Company.Identity.Application.Event.Interfaces.Dispatchers;
using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Repositories;
using Company.Identity.Domain.User.Interfaces.Specifications;
using Company.Identity.Shared.Results;

namespace Company.Identity.Application.Auth.Handlers;

public class CreateUserHandler(
    IAuthService authService,
    IEventDispatcher dispatcher,
    IUserSpecification userSpecification,
    IUserRepository userRepository) : ICreateUserHandler
{
    public async Task<OperationResult<CreateUserDto>> HandleAsync(CreateUserCommand command)
    {
        var userExistsSpec = userSpecification.HasUserNameAndEmail(command.UserName, command.Email);
        var userAlreadyExists = await userRepository.AnyAsync(userExistsSpec);

        if (userAlreadyExists.Value)
            return OperationResult<CreateUserDto>.Fail(
                "A user with the same email and username already exists.",
                HttpStatusCode.Conflict
            );

        var passwordHash = authService.HashPassword(command.Password);
        var userEntity = new UserEntity(command.UserName, command.Email, passwordHash);

        var createUserResult = await userRepository.AddAsync(userEntity);
        if (!createUserResult.IsSuccess)
            return OperationResult<CreateUserDto>.FailFrom(createUserResult);

        var userCreatedEvent = new UserCreatedEvent(createUserResult.Value.Email, createUserResult.Value.UserName);
        await dispatcher.DispatchAsync(userCreatedEvent);
        var userDto = CreateUserDto.FromEntity(createUserResult.Value);
        return OperationResult<CreateUserDto>.Ok(userDto);
    }
}