namespace EmployeeManagement.Application.Features.Employees.DTOs;

public class CreateEmployeeRequest
{
    public string IdentificationNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public int CurrentPositionId { get; set; }
    public int DepartmentId { get; set; }
    public DateTime HireDate { get; set; }
}
