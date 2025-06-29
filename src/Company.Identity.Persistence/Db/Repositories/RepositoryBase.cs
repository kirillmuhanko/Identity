using Company.Identity.Domain.Common.Entities;
using Company.Identity.Shared.Result.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Company.Identity.Persistence.Db.Repositories;

public abstract class RepositoryBase<TEntity>(
    IValidator<TEntity> validator,
    DbContext context,
    ILogger<RepositoryBase<TEntity>> logger)
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<ResultModel<TEntity>> GetByIdAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
            return ResultModel<TEntity>.Fail(
                "The requested item could not be found.",
                404
            );

        return ResultModel<TEntity>.Ok(entity);
    }

    public virtual async Task<ResultModel<TEntity>> AddAsync(TEntity entity)
    {
        var validationResult = await validator.ValidateAsync(entity);
        if (!validationResult.IsValid)
        {
            var result = ResultModel<TEntity>.Fail(
                "The request could not be processed due to validation errors."
            );

            foreach (var error in validationResult.Errors) 
                result.AddError(error.PropertyName, error.ErrorMessage);

            return result;
        }

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
                500
            );
        }
    }

    public virtual async Task<ResultModel<TEntity>> UpdateAsync(TEntity entity)
    {
        var validationResult = await validator.ValidateAsync(entity);
        if (!validationResult.IsValid)
        {
            var result = ResultModel<TEntity>.Fail(
                "The request could not be processed due to validation errors."
            );

            foreach (var error in validationResult.Errors) 
                result.AddError(error.PropertyName, error.ErrorMessage);

            return result;
        }

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
                500
            );
        }
    }

    public virtual async Task<ResultModel<bool>> DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
            return ResultModel<bool>.Fail(
                "The item you're trying to delete does not exist.",
                404
            );

        try
        {
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
                500
            );
        }
    }
}