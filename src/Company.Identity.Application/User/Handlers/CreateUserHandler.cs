using AutoMapper;
using Company.Identity.Application.Event.Interfaces.Dispatchers;
using Company.Identity.Application.User.Commands;
using Company.Identity.Application.User.DTOs;
using Company.Identity.Application.User.Events;
using Company.Identity.Application.User.Interfaces.Handlers;
using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Repositories;
using Company.Identity.Domain.User.Interfaces.Specifications;
using Company.Identity.Shared.Result.Models;

namespace Company.Identity.Application.User.Handlers;

public class CreateUserHandler(
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
                409 // Conflict
            );

        var userEntity = new UserEntity(command.UserName, command.Email);
        var result = await userRepository.AddAsync(userEntity);

        if (!result.IsSuccess)
            return ResultModel<CreateUserDto>.FailFrom(result);

        var userCreatedEvent = new UserCreatedEvent(result.Value.Email, result.Value.UserName);
        await dispatcher.DispatchAsync(userCreatedEvent);
        var createUserDto = mapper.Map<CreateUserDto>(result.Value);
        return ResultModel<CreateUserDto>.Ok(createUserDto);
    }
}