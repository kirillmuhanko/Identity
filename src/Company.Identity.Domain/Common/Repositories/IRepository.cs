using System.Linq.Expressions;
using Company.Identity.Shared.Result.Models;

namespace Company.Identity.Domain.Common.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<ResultModel<bool>> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    Task<ResultModel<TEntity>> GetByIdAsync(Guid id);

    Task<ResultModel<TEntity>> AddAsync(TEntity entity);

    Task<ResultModel<TEntity>> UpdateAsync(TEntity entity);

    Task<ResultModel<bool>> DeleteAsync(Guid id);
}