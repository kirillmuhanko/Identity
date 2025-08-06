using System.Net;
using Company.Identity.Application.Auth.Commands;
using Company.Identity.Application.Auth.DTOs;
using Company.Identity.Application.Auth.Events;
using Company.Identity.Application.Auth.Interfaces.Handlers;
using Company.Identity.Application.Auth.Interfaces.Services;
using Company.Identity.Application.Event.Interfaces.Dispatchers;
using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.DomainServices;
using Company.Identity.Domain.User.Interfaces.Repositories;
using Company.Identity.Domain.User.Interfaces.Specifications;
using Company.Identity.Shared.ResultPattern.Results;

namespace Company.Identity.Application.Auth.Handlers;

public class CreateUserHandler(
    IAuthService authService,
    IUserService userService,
    IEventDispatcher dispatcher,
    IUserSpecification userSpecification,
    IUserRepository userRepository) : ICreateUserHandler
{
    public async Task<OperationResult<CreateUserDto>> HandleAsync(CreateUserCommand command)
    {
        if (!userService.IsPasswordStrong(command.Password))
            return OperationResult<CreateUserDto>.Fail(
                "Password is not strong enough. It must be at least 8 characters and include uppercase, lowercase, number, and symbol.");

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

        var jwtToken = authService.GenerateJwtToken(userEntity);
        var userDto = CreateUserDto.From(createUserResult.Value, jwtToken);

        var userCreatedEvent = new UserCreatedEvent(userDto.Email, userDto.UserName);
        await dispatcher.DispatchAsync(userCreatedEvent);

        return OperationResult<CreateUserDto>.Ok(userDto);
    }
}