using System.Linq.Expressions;
using Company.Identity.Domain.User.Entities;
using Company.Identity.Domain.User.Interfaces.Specifications;

namespace Company.Identity.Domain.User.Specifications;

public class UserSpecification : IUserSpecification
{
    public Expression<Func<UserEntity, bool>> HasEmail(string email)
    {
        return user => user.Email == email;
    }

    public Expression<Func<UserEntity, bool>> HasUserName(string userName)
    {
        return user => user.UserName == userName;
    }

    public Expression<Func<UserEntity, bool>> HasUserNameAndEmail(string userName, string email)
    {
        return user => user.UserName == userName && user.Email == email;
    }

    public Expression<Func<UserEntity, bool>> IsActive()
    {
        return user => user.IsActive;
    }
}