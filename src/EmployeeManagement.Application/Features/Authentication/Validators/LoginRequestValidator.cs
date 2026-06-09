namespace EmployeeManagement.Application.Features.Authentication.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
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
            .WithMessage($"Password must be at least {DomainValidationConstants.PasswordMinLength} characters.");
    }
}
