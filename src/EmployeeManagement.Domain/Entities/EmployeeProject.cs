namespace EmployeeManagement.Domain.Entities;

public class EmployeeProject
{
    public int Id { get; internal set; }
    public int EmployeeId { get; private set; }
    public int ProjectId { get; private set; }
    public DateTime AssignedDate { get; private set; }
    public DateTime? UnassignedDate { get; private set; }

    private EmployeeProject() { }

    public EmployeeProject(int employeeId, int projectId, DateTime assignedDate)
    {
        EmployeeId = employeeId;
        ProjectId = projectId;
        AssignedDate = assignedDate;
    }

    public void SetUnassignedDate(DateTime unassignedDate)
    {
        if (unassignedDate < AssignedDate)
            throw new ArgumentException("Unassigned date cannot be before assigned date.", nameof(unassignedDate));

        UnassignedDate = unassignedDate;
    }

    public void MarkAsAssigned()
    {
        UnassignedDate = null;
    }
}
