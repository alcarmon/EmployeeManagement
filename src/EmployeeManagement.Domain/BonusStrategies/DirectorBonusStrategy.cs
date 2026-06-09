namespace EmployeeManagement.Domain.BonusStrategies;

public class DirectorBonusStrategy : IBonusCalculationStrategy
{
    public string StrategyName => PositionNames.Director;

    public decimal CalculateBonus(decimal salary)
    {
        return salary * BonusConstants.DirectorPercentage;
    }
}
