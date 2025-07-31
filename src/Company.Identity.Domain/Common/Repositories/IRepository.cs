using System.Linq.Expressions;
using Company.Identity.Shared.ResultPattern.Results;

namespace Company.Identity.Domain.Common.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<OperationResult<bool>> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    Task<OperationResult<TEntity>> GetByIdAsync(Guid id);

    Task<OperationResult<TEntity>> AddAsync(TEntity entity);

    Task<OperationResult<TEntity>> UpdateAsync(TEntity entity);

    Task<OperationResult<bool>> DeleteAsync(Guid id);
}