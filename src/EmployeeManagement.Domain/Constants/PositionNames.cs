namespace EmployeeManagement.Domain.Constants;

public static class PositionNames
{
    public const string Employee = "Employee";
    public const string Manager = "Manager";
    public const string SeniorManager = "Senior Manager";
    public const string Director = "Director";

    public static readonly List<string> AllPositions = new()
    {
        Employee,
        Manager,
        SeniorManager,
        Director
    };

    public static bool IsValid(string positionName) => AllPositions.Contains(positionName);
}
