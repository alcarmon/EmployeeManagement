namespace EmployeeManagement.Domain.Tests;

public class EmployeeTests
{
    [Fact]
    public void ChangePosition_ShouldRecordPreviousPositionInHistory()
    {
        Employee employee = new Employee(
            identificationNumber: "123456789",
            name: "Martin",
            salary: 4500m,
            currentPositionId: 1,
            departmentId: 1,
            hireDate: DateTime.UtcNow.AddDays(-30));

        employee.ChangePosition(2);

        Assert.Equal(2, employee.CurrentPositionId);
        Assert.Single(employee.PositionHistory);
        Assert.Equal(1, employee.PositionHistory.First().PositionId);
    }
}
