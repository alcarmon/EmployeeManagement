namespace EmployeeManagement.Application.Features.Authentication.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be valid.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(DomainValidationConstants.PasswordMinLength)
            .WithMessage($"Password must be at least {DomainValidationConstants.PasswordMinLength} characters.")
            .Matches(DomainValidationConstants.PasswordUppercasePattern)
            .WithMessage("Password must contain at least one uppercase letter.")
            .Matches(DomainValidationConstants.PasswordDigitPattern)
            .WithMessage("Password must contain at least one digit.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Confirm password is required.")
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match.");
    }
}
