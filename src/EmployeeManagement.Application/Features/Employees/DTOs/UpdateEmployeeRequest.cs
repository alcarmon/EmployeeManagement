namespace EmployeeManagement.Application.Features.Employees.DTOs;

public class UpdateEmployeeRequest
{
    public string? IdentificationNumber { get; set; }
    public string? Name { get; set; }
    public decimal? Salary { get; set; }
    public int? PositionId { get; set; }
    public int? DepartmentId { get; set; }
    public List<int>? ProjectIds { get; set; }
}
