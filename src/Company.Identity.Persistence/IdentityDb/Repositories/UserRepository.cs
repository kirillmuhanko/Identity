using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Repositories;
using Company.Identity.Persistence.Db.Repositories;
using Company.Identity.Persistence.IdentityDb.Contexts;
using Microsoft.Extensions.Logging;

namespace Company.Identity.Persistence.IdentityDb.Repositories;

public class UserRepository(
    IdentityDbContext context,
    ILogger<UserRepository> logger)
    : RepositoryBase<UserEntity>(context, logger), IUserRepository;