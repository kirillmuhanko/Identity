using Company.Identity.Domain.User.Interfaces.Validators;

namespace Company.Identity.Domain.User.Validators;

public class PasswordValidator : IPasswordValidator
{
    public bool IsPasswordStrong(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        var hasUpper = password.Any(char.IsUpper);
        var hasLower = password.Any(char.IsLower);
        var hasDigit = password.Any(char.IsDigit);
        var hasSymbol = password.Any(ch => !char.IsLetterOrDigit(ch));
        var hasMinLength = password.Length >= 8;
        var isStrong = hasUpper && hasLower && hasDigit && hasSymbol && hasMinLength;
        return isStrong;
    }
}