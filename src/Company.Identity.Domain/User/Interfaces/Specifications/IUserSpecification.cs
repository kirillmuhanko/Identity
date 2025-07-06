using System.Linq.Expressions;
using Company.Identity.Domain.User.Entities;

namespace Company.Identity.Domain.User.Interfaces.Specifications;

public interface IUserSpecification
{
    Expression<Func<UserEntity, bool>> HasEmail(string email);

    Expression<Func<UserEntity, bool>> HasUserName(string userName);

    Expression<Func<UserEntity, bool>> HasUserNameAndEmail(string userName, string email);
}