using Company.Identity.Domain.Common.Entities;
using Company.Identity.Shared.Result.Constants;
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
                ErrorCodes.NotFound,
                "The requested item could not be found."
            );

        return ResultModel<TEntity>.Ok(entity);
    }

    public virtual async Task<ResultModel<TEntity>> AddAsync(TEntity entity)
    {
        var validationResult = await validator.ValidateAsync(entity);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new ErrorDetailModel
            {
                Code = ErrorCodes.Validation,
                Message = e.ErrorMessage
            }).ToList();

            return ResultModel<TEntity>.Fail(errors);
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
                ErrorCodes.SaveFailed,
                "We couldn't save your changes. Please try again."
            );
        }
    }

    public virtual async Task<ResultModel<TEntity>> UpdateAsync(TEntity entity)
    {
        var validationResult = await validator.ValidateAsync(entity);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new ErrorDetailModel
            {
                Code = ErrorCodes.Validation,
                Message = e.ErrorMessage
            }).ToList();

            return ResultModel<TEntity>.Fail(errors);
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
                ErrorCodes.UpdateFailed,
                "We couldn't update the item. Please try again later."
            );
        }
    }

    public virtual async Task<ResultModel<bool>> DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
            return ResultModel<bool>.Fail(
                ErrorCodes.NotFound,
                "The item you're trying to delete does not exist."
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
                ErrorCodes.DeleteFailed,
                "We couldn't delete the item at this time. Please try again."
            );
        }
    }
}