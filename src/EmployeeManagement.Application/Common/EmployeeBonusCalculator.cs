namespace EmployeeManagement.Application.Common;

public static class EmployeeBonusCalculator
{
    public static EmployeeBonusCalculation Calculate(
        Employee employee,
        string positionName,
        IBonusStrategyFactory bonusStrategyFactory,
        DateTime currentDate)
    {
        bool isEligible = employee.HireDate.Date <= currentDate.Date.AddYears(-BonusConstants.MinimumTenureYears);
        if (!isEligible)
        {
            return new EmployeeBonusCalculation(false, decimal.Zero, ApplicationConstants.NotEligibleBonusReason);
        }

        IBonusCalculationStrategy strategy = bonusStrategyFactory.CreateStrategy(positionName);
        decimal bonusAmount = strategy.CalculateBonus(employee.Salary);

        return new EmployeeBonusCalculation(true, bonusAmount, string.Empty);
    }
}
