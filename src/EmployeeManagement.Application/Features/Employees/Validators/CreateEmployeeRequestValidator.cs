namespace EmployeeManagement.Application.Features.Employees.Validators;

public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator()
    {
        RuleFor(x => x.IdentificationNumber)
            .NotEmpty()
            .WithMessage("Employee identification number is required.")
            .MaximumLength(DomainValidationConstants.EmployeeIdentificationNumberMaxLength)
            .WithMessage($"Employee identification number cannot exceed {DomainValidationConstants.EmployeeIdentificationNumberMaxLength} characters.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Employee name is required.")
            .MinimumLength(DomainValidationConstants.MinimumTextLength)
            .WithMessage($"Employee name must be at least {DomainValidationConstants.MinimumTextLength} characters.")
            .MaximumLength(DomainValidationConstants.EmployeeNameMaxLength)
            .WithMessage($"Employee name cannot exceed {DomainValidationConstants.EmployeeNameMaxLength} characters.");

        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(DomainValidationConstants.MinimumSalary)
            .WithMessage("Salary cannot be negative.");

        RuleFor(x => x.CurrentPositionId)
            .GreaterThanOrEqualTo(DomainValidationConstants.MinimumEntityId)
            .WithMessage("A valid position must be selected.");

        RuleFor(x => x.DepartmentId)
            .GreaterThanOrEqualTo(DomainValidationConstants.MinimumEntityId)
            .WithMessage("A valid department must be selected.");

        RuleFor(x => x.HireDate)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Hire date cannot be in the future.");
    }
}
