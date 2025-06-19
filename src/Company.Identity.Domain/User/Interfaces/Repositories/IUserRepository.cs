using Company.Identity.Domain.User.Entities;
using Company.Identity.Shared.Result.Models;

namespace Company.Identity.Domain.User.Interfaces.Repositories;

public interface IUserRepository
{
    Task<ResultModel<UserEntity>> GetByIdAsync(Guid id);

    Task<ResultModel<UserEntity>> AddAsync(UserEntity user);

    Task<ResultModel<UserEntity>> UpdateAsync(UserEntity user);

    Task<ResultModel<bool>> DeleteAsync(Guid id);
}