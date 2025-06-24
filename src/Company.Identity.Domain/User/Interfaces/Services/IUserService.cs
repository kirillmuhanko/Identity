using Company.Identity.Domain.User.Entities;
using Company.Identity.Shared.Result.Models;

namespace Company.Identity.Domain.User.Interfaces.Services;

public interface IUserService
{
    Task<ResultModel<UserEntity>> AddUserAsync(UserEntity user);
}