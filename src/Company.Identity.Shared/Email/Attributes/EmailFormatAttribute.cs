using System.ComponentModel.DataAnnotations;
using Company.Identity.Shared.Email.Validators;

namespace Company.Identity.Shared.Email.Attributes;

public class EmailFormatAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var email = value as string;

        if (!string.IsNullOrWhiteSpace(email) && EmailValidator.IsEmailFormatValid(email))
            return ValidationResult.Success;

        var errorMessage = FormatErrorMessage(validationContext.DisplayName);
        return new ValidationResult(errorMessage);
    }
}