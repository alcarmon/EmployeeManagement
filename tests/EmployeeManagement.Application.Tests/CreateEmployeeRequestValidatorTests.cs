namespace EmployeeManagement.Application.Tests;

public class CreateEmployeeRequestValidatorTests
{
    private readonly CreateEmployeeRequestValidator _validator = new();

    [Fact]
    public void Validate_GivenInvalidName_ShouldHaveValidationError()
    {
        CreateEmployeeRequest request = new CreateEmployeeRequest
        {
            Name = string.Empty,
            Salary = 1000m,
            CurrentPositionId = 1,
            DepartmentId = 1,
            HireDate = DateTime.UtcNow.Date
        };

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == "Name");
    }
}
