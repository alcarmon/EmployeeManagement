namespace EmployeeManagement.Domain.Entities;

public class PositionHistory
{
    public int Id { get; internal set; }
    public int EmployeeId { get; private set; }
    public int PositionId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    private PositionHistory() { }

    public PositionHistory(int employeeId, int positionId, DateTime startDate)
    {
        EmployeeId = employeeId;
        PositionId = positionId;
        StartDate = startDate;
    }

    public void SetEndDate(DateTime endDate)
    {
        if (endDate < StartDate)
            throw new ArgumentException("End date cannot be before start date.", nameof(endDate));

        EndDate = endDate;
    }
}
