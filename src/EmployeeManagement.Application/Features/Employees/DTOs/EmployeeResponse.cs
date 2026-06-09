namespace EmployeeManagement.Application.Features.Employees.DTOs;

public class EmployeeResponse
{
    public int Id { get; set; }
    public string IdentificationNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public int CurrentPositionId { get; set; }
    public string CurrentPositionName { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public bool IsBonusEligible { get; set; }
    public decimal BonusAmount { get; set; }
    public bool IsActive { get; set; }
}
