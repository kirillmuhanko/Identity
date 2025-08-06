using System.Text.RegularExpressions;
using Company.Identity.Domain.User.Interfaces.Validators;

namespace Company.Identity.Domain.User.Validators;

public class EmailValidator : IEmailValidator
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled,
        TimeSpan.FromMilliseconds(250)
    );

    public bool IsFormatValid(string email)
    {
        var isEmpty = string.IsNullOrWhiteSpace(email);
        var matchesPattern = !isEmpty && EmailRegex.IsMatch(email);
        return matchesPattern;
    }
}