namespace EmployeeManagement.Domain.BonusStrategies;

public class RegularEmployeeBonusStrategy : IBonusCalculationStrategy
{
    public string StrategyName => BonusConstants.RegularEmployeeStrategyName;

    public decimal CalculateBonus(decimal salary)
    {
        return salary * BonusConstants.RegularEmployeePercentage;
    }
}
