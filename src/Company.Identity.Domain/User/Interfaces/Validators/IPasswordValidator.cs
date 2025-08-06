namespace Company.Identity.Domain.User.Interfaces.Validators;

public interface IPasswordValidator
{
    bool IsPasswordStrong(string password);
}