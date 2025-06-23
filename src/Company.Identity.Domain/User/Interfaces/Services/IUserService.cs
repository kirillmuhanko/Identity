using Company.Identity.Domain.User.Entities;

namespace Company.Identity.Domain.User.Interfaces.Services;

public interface IUserService
{
    Task AddUserAsync(UserEntity user);
}