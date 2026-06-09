namespace EmployeeManagement.Domain.BonusStrategies;

public interface IBonusCalculationStrategy
{
    decimal CalculateBonus(decimal salary);
    string StrategyName { get; }
}
