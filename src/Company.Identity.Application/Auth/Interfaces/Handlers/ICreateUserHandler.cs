using Company.Identity.Application.Auth.Commands;
using Company.Identity.Application.Auth.DTOs;
using Company.Identity.Shared.Result.Models;

namespace Company.Identity.Application.Auth.Interfaces.Handlers;

public interface ICreateUserHandler
{
    Task<ResultModel<CreateUserDto>> HandleAsync(CreateUserCommand command);
}