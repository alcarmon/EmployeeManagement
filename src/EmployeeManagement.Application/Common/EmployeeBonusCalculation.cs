namespace EmployeeManagement.Application.Common;

public sealed record EmployeeBonusCalculation(
    bool IsEligible,
    decimal BonusAmount,
    string IneligibilityReason);
