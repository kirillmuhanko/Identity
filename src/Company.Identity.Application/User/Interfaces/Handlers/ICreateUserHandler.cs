using Company.Identity.Application.User.Commands;
using Company.Identity.Application.User.DTOs;
using Company.Identity.Shared.Result.Models;

namespace Company.Identity.Application.User.Interfaces.Handlers;

public interface ICreateUserHandler
{
    Task<ResultModel<CreateUserDto>> HandleAsync(CreateUserCommand command);
}