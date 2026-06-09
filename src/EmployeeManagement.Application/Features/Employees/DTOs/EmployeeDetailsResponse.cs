namespace EmployeeManagement.Application.Features.Employees.DTOs;

public class EmployeeDetailsResponse
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
    public string BonusIneligibilityReason { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<PositionHistoryDto> PositionHistory { get; set; } = new();
    public List<ProjectDto> Projects { get; set; } = new();

    public class PositionHistoryDto
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public DateTime AssignedDate { get; set; }
        public DateTime? UnassignedDate { get; set; }
    }
}
