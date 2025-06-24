using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Repositories;
using Company.Identity.Domain.User.Interfaces.Services;
using Company.Identity.Shared.Result.Models;

namespace Company.Identity.Domain.User.Services;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task<ResultModel<UserEntity>> AddUserAsync(UserEntity user)
    {
        var result = await repository.AddAsync(user);
        return result;
    }
}