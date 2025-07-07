using System.Linq.Expressions;
using System.Net;
using Company.Identity.Domain.Common.Entities;
using Company.Identity.Shared.Result.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Company.Identity.Persistence.Db.Repositories;

public abstract class RepositoryBase<TEntity>(
    DbContext context,
    ILogger<RepositoryBase<TEntity>> logger)
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<ResultModel<bool>> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var exists = await _dbSet.AnyAsync(predicate);
            return ResultModel<bool>.Ok(exists);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while checking existence of {EntityType}: {Message}",
                typeof(TEntity).Name, ex.Message);

            return ResultModel<bool>.Fail(
                "Unable to verify existence at this time.",
                HttpStatusCode.InternalServerError
            );
        }
    }

    public virtual async Task<ResultModel<TEntity>> GetByIdAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
            return ResultModel<TEntity>.Fail(
                "The requested item could not be found.",
                HttpStatusCode.NotFound
            );

        return ResultModel<TEntity>.Ok(entity);
    }

    public virtual async Task<ResultModel<TEntity>> AddAsync(TEntity entity)
    {
        try
        {
            _dbSet.Add(entity);
            await context.SaveChangesAsync();
            return ResultModel<TEntity>.Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while adding {EntityType}: {Message}",
                typeof(TEntity).Name, ex.Message);

            return ResultModel<TEntity>.Fail(
                "We couldn't save your changes. Please try again.",
                HttpStatusCode.InternalServerError
            );
        }
    }

    public virtual async Task<ResultModel<TEntity>> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            await context.SaveChangesAsync();
            return ResultModel<TEntity>.Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while updating {EntityType}: {Message}",
                typeof(TEntity).Name, ex.Message);

            return ResultModel<TEntity>.Fail(
                "We couldn't update the item. Please try again later.",
                HttpStatusCode.InternalServerError
            );
        }
    }

    public virtual async Task<ResultModel<bool>> DeleteAsync(Guid id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null)
                return ResultModel<bool>.Fail(
                    "The item you're trying to delete does not exist.",
                    HttpStatusCode.NotFound
                );

            _dbSet.Remove(entity);
            await context.SaveChangesAsync();
            return ResultModel<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while deleting {EntityType}: {Message}",
                typeof(TEntity).Name, ex.Message);

            return ResultModel<bool>.Fail(
                "We couldn't delete the item at this time. Please try again.",
                HttpStatusCode.InternalServerError
            );
        }
    }
}