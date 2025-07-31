using Company.Identity.Application.Auth.Commands;
using Company.Identity.Application.Auth.DTOs;
using Company.Identity.Shared.ResultPattern.Results;

namespace Company.Identity.Application.Auth.Interfaces.Handlers;

public interface ICreateUserHandler
{
    Task<OperationResult<CreateUserDto>> HandleAsync(CreateUserCommand command);
}