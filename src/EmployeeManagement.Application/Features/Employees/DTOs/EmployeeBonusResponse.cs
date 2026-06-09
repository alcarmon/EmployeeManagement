namespace EmployeeManagement.Application.Features.Employees.DTOs;

public class EmployeeBonusResponse
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string CurrentPositionName { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsBonusEligible { get; set; }
    public decimal BonusAmount { get; set; }
    public string IneligibilityReason { get; set; } = string.Empty;
}
