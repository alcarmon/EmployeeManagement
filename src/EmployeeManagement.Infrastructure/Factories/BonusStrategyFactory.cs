namespace EmployeeManagement.Infrastructure.Factories;

public class BonusStrategyFactory : IBonusStrategyFactory
{
    public IBonusCalculationStrategy CreateStrategy(string positionName)
    {
        if (string.IsNullOrWhiteSpace(positionName))
            throw new ArgumentException("Position name cannot be empty.", nameof(positionName));

        return positionName switch
        {
            PositionNames.Employee => new RegularEmployeeBonusStrategy(),
            PositionNames.Manager => new ManagerBonusStrategy(),
            PositionNames.SeniorManager => new SeniorManagerBonusStrategy(),
            PositionNames.Director => new DirectorBonusStrategy(),
            _ => new RegularEmployeeBonusStrategy() // Default fallback
        };
    }
}
