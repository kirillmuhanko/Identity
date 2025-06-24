using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Repositories;
using Company.Identity.Persistence.IdentityDb.Contexts;
using Company.Identity.Shared.Result.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Company.Identity.Persistence.IdentityDb.Repositories;

public class UserRepository(
    IValidator<UserEntity> validator,
    IdentityDbContext context,
    ILogger<UserRepository> logger) : IUserRepository
{
    public async Task<ResultModel<UserEntity>> GetByIdAsync(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        if (user is null)
            return ResultModel<UserEntity>.Fail(
                "USER_NOT_FOUND",
                "User not found."
            );

        return ResultModel<UserEntity>.Ok(user);
    }

    public async Task<ResultModel<UserEntity>> AddAsync(UserEntity user)
    {
        var validationResult = await validator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new ErrorDetailModel
            {
                Code = "VALIDATION_ERROR",
                Message = e.ErrorMessage
            }).ToList();

            return ResultModel<UserEntity>.Fail(errors);
        }

        try
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return ResultModel<UserEntity>.Ok(user);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error while adding user: {Message}", ex.Message);

            return ResultModel<UserEntity>.Fail(
                "DB_SAVE_FAILED",
                "Failed to save user."
            );
        }
    }

    public async Task<ResultModel<UserEntity>> UpdateAsync(UserEntity user)
    {
        var validationResult = await validator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new ErrorDetailModel
            {
                Code = "VALIDATION_ERROR",
                Message = e.ErrorMessage
            }).ToList();

            return ResultModel<UserEntity>.Fail(errors);
        }

        try
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return ResultModel<UserEntity>.Ok(user);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error while updating user: {Message}", ex.Message);

            return ResultModel<UserEntity>.Fail(
                "DB_UPDATE_FAILED",
                "Failed to update user."
            );
        }
    }

    public async Task<ResultModel<bool>> DeleteAsync(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        if (user is null)
            return ResultModel<bool>.Fail(
                "USER_NOT_FOUND",
                "User not found."
            );

        try
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return ResultModel<bool>.Ok(true);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error while deleting user: {Message}", ex.Message);

            return ResultModel<bool>.Fail(
                "DB_DELETE_FAILED",
                "Failed to delete user."
            );
        }
    }
}