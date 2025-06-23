using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Repositories;
using Company.Identity.Domain.User.Interfaces.Services;

namespace Company.Identity.Domain.User.Services;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task AddUserAsync(UserEntity user)
    {
        await repository.AddAsync(user);
    }
}