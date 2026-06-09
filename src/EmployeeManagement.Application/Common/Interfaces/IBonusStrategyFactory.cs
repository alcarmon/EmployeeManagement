namespace EmployeeManagement.Application.Common.Interfaces;

public interface IBonusStrategyFactory
{
    IBonusCalculationStrategy CreateStrategy(string positionName);
}
