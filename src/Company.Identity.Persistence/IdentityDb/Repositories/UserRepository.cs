using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Repositories;
using Company.Identity.Persistence.Common.Repositories;
using Company.Identity.Persistence.IdentityDb.Contexts;
using Microsoft.Extensions.Logging;

namespace Company.Identity.Persistence.IdentityDb.Repositories;

public class UserRepository(
    IdentityDbContext context,
    ILogger<UserRepository> logger)
    : RepositoryBase<UserEntity>(context, logger), IUserRepository
{
    protected override string EntityDisplayName => "user";
}