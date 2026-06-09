namespace EmployeeManagement.Domain.BonusStrategies;

public class SeniorManagerBonusStrategy : IBonusCalculationStrategy
{
    public string StrategyName => PositionNames.SeniorManager;

    public decimal CalculateBonus(decimal salary)
    {
        return salary * BonusConstants.SeniorManagerPercentage;
    }
}
