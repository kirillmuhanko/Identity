namespace Company.Identity.Domain.User.Interfaces.DomainServices;

public interface IUserService
{
    bool IsPasswordStrong(string password);
}