namespace EmployeeManagement.Domain.BonusStrategies;

public class ManagerBonusStrategy : IBonusCalculationStrategy
{
    public string StrategyName => PositionNames.Manager;

    public decimal CalculateBonus(decimal salary)
    {
        return salary * BonusConstants.ManagerPercentage;
    }
}
