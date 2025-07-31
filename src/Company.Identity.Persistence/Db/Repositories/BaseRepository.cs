using System.Linq.Expressions;
using System.Net;
using Company.Identity.Domain.Common.Entities;
using Company.Identity.Shared.ResultPattern.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Company.Identity.Persistence.Db.Repositories;

public abstract class RepositoryBase<TEntity>(
    DbContext context,
    ILogger<RepositoryBase<TEntity>> logger)
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    protected virtual string EntityDisplayName => "item";

    public virtual async Task<OperationResult<bool>> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var exists = await _dbSet.AnyAsync(predicate);
            return OperationResult<bool>.Ok(exists);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while checking existence of {EntityType}: {Message}",
                typeof(TEntity).Name, ex.Message);

            return OperationResult<bool>.Fail(
                $"Unable to verify {EntityDisplayName} existence at this time.",
                HttpStatusCode.InternalServerError
            );
        }
    }

    public virtual async Task<OperationResult<TEntity>> GetByIdAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
            return OperationResult<TEntity>.Fail(
                $"The requested {EntityDisplayName} could not be found.",
                HttpStatusCode.NotFound
            );

        return OperationResult<TEntity>.Ok(entity);
    }

    public virtual async Task<OperationResult<TEntity>> AddAsync(TEntity entity)
    {
        try
        {
            _dbSet.Add(entity);
            await SaveAuditableChangesAsync();
            return OperationResult<TEntity>.Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while adding {EntityType}: {Message}",
                typeof(TEntity).Name, ex.Message);

            return OperationResult<TEntity>.Fail(
                $"We couldn't save the {EntityDisplayName}. Please try again.",
                HttpStatusCode.InternalServerError
            );
        }
    }

    public virtual async Task<OperationResult<TEntity>> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            await SaveAuditableChangesAsync();
            return OperationResult<TEntity>.Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while updating {EntityType}: {Message}",
                typeof(TEntity).Name, ex.Message);

            return OperationResult<TEntity>.Fail(
                $"We couldn't update the {EntityDisplayName}. Please try again later.",
                HttpStatusCode.InternalServerError
            );
        }
    }

    public virtual async Task<OperationResult<bool>> DeleteAsync(Guid id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null)
                return OperationResult<bool>.Fail(
                    $"The {EntityDisplayName} you're trying to delete does not exist.",
                    HttpStatusCode.NotFound
                );

            _dbSet.Remove(entity);
            await SaveAuditableChangesAsync();
            return OperationResult<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while deleting {EntityType}: {Message}",
                typeof(TEntity).Name, ex.Message);

            return OperationResult<bool>.Fail(
                $"We couldn't delete the {EntityDisplayName} at this time. Please try again.",
                HttpStatusCode.InternalServerError
            );
        }
    }

    private async Task SaveAuditableChangesAsync()
    {
        var entries = context.ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            var now = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = now;
            else if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAt = now;
        }

        await context.SaveChangesAsync();
    }
}