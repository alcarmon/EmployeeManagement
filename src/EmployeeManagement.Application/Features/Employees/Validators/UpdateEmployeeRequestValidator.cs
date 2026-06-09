namespace EmployeeManagement.Application.Features.Employees.Validators;

public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
{
    public UpdateEmployeeRequestValidator()
    {
        RuleFor(x => x.IdentificationNumber)
            .MaximumLength(DomainValidationConstants.EmployeeIdentificationNumberMaxLength)
            .WithMessage($"Employee identification number cannot exceed {DomainValidationConstants.EmployeeIdentificationNumberMaxLength} characters.")
            .When(x => !string.IsNullOrEmpty(x.IdentificationNumber));

        RuleFor(x => x.Name)
            .MinimumLength(DomainValidationConstants.MinimumTextLength)
            .WithMessage($"Employee name must be at least {DomainValidationConstants.MinimumTextLength} characters.")
            .MaximumLength(DomainValidationConstants.EmployeeNameMaxLength)
            .WithMessage($"Employee name cannot exceed {DomainValidationConstants.EmployeeNameMaxLength} characters.")
            .When(x => !string.IsNullOrEmpty(x.Name));

        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(DomainValidationConstants.MinimumSalary)
            .WithMessage("Salary cannot be negative.")
            .When(x => x.Salary.HasValue);

        RuleFor(x => x.PositionId)
            .GreaterThanOrEqualTo(DomainValidationConstants.MinimumEntityId)
            .WithMessage("A valid position must be selected.")
            .When(x => x.PositionId.HasValue);

        RuleFor(x => x.DepartmentId)
            .GreaterThanOrEqualTo(DomainValidationConstants.MinimumEntityId)
            .WithMessage("A valid department must be selected.")
            .When(x => x.DepartmentId.HasValue);

        RuleForEach(x => x.ProjectIds)
            .GreaterThanOrEqualTo(DomainValidationConstants.MinimumEntityId)
            .WithMessage($"Project IDs must be at least {DomainValidationConstants.MinimumEntityId}.")
            .When(x => x.ProjectIds != null);

        RuleFor(x => x.ProjectIds)
            .Must(projectIds => projectIds == null || projectIds.Count == projectIds.Distinct().Count())
            .WithMessage("Project IDs cannot contain duplicates.");
    }
}
