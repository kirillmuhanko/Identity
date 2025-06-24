using Company.Identity.Domain.User.Entities;
using FluentValidation;

namespace Company.Identity.Persistence.IdentityDb.Validators;

public class UserEntityValidator : AbstractValidator<UserEntity>
{
    public UserEntityValidator()
    {
        RuleFor(user => user.UserName)
            .NotEmpty().WithMessage("User name is required.")
            .MinimumLength(3).WithMessage("User name must be at least 3 characters long.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.");
    }
}