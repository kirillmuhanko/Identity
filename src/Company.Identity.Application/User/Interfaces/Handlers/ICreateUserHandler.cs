using Company.Identity.Application.User.Commands;
using Company.Identity.Application.User.DTOs;
using Company.Identity.Shared.ResultPattern.Results;

namespace Company.Identity.Application.User.Interfaces.Handlers;

public interface ICreateUserHandler
{
    Task<OperationResult<CreateUserDto>> HandleAsync(CreateUserCommand command);
}