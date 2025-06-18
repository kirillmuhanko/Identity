using Company.Identity.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.Identity.Persistence.Contexts;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
}