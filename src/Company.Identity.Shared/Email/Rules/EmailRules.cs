using System.Text.RegularExpressions;

namespace Company.Identity.Shared.Email.Rules;

public static partial class EmailRules
{
    private static readonly Regex EmailRegex = MyRegex();

    public static bool IsEmailFormatValid(string email)
    {
        return !string.IsNullOrWhiteSpace(email) && EmailRegex.IsMatch(email);
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex MyRegex();
}