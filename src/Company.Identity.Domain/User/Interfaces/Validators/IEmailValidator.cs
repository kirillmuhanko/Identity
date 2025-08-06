namespace Company.Identity.Domain.User.Interfaces.Validators;

public interface IEmailValidator
{
    bool IsFormatValid(string email);
}