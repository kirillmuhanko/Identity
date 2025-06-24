using Company.Identity.Application.User.Commands;
using Company.Identity.Shared.Result.Models;

namespace Company.Identity.Application.User.Interfaces.Handlers;

public interface ICreateUserHandler
{
    Task<ResultModel<bool>> HandleAsync(CreateUserCommand command);
}