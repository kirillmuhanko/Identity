using Company.Identity.Domain.Common.Repositories;
using Company.Identity.Domain.User.Entities;

namespace Company.Identity.Domain.User.Interfaces.Repositories;

public interface IUserRepository : IRepository<UserEntity>;