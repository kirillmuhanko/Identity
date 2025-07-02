using System.Linq.Expressions;
using Company.Identity.Domain.User.Entities;

namespace Company.Identity.Domain.User.Specifications;

public class UserSpecification(UserEntity inputUser)
{
    public Expression<Func<UserEntity, bool>> UserWithEmailAndUsernameExistsSpec()
    {
        return u => u.Email == inputUser.Email && u.UserName == inputUser.UserName;
    }
}