namespace EmployeeManagement.Application.Features.Employees.Queries;

public class GetEmployeeBonusQuery : IRequest<EmployeeBonusResponse>
{
    public int EmployeeId { get; set; }
}

public class GetEmployeeBonusQueryHandler : IRequestHandler<GetEmployeeBonusQuery, EmployeeBonusResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBonusStrategyFactory _bonusStrategyFactory;

    public GetEmployeeBonusQueryHandler(
        IUnitOfWork unitOfWork,
        IBonusStrategyFactory bonusStrategyFactory)
    {
        _unitOfWork = unitOfWork;
        _bonusStrategyFactory = bonusStrategyFactory;
    }

    public async Task<EmployeeBonusResponse> Handle(GetEmployeeBonusQuery request, CancellationToken cancellationToken)
    {
        Employee? employee = await _unitOfWork.Employees.GetByIdAsync(request.EmployeeId);
        if (employee == null)
            throw new NotFoundException($"Employee with ID {request.EmployeeId} was not found.");

        Position? position = await _unitOfWork.Positions.GetByIdAsync(employee.CurrentPositionId);
        string positionName = position?.Name ?? ApplicationConstants.UnknownValue;
        EmployeeBonusCalculation bonusCalculation = EmployeeBonusCalculator.Calculate(
            employee,
            positionName,
            _bonusStrategyFactory,
            DateTime.UtcNow);

        return new EmployeeBonusResponse
        {
            EmployeeId = employee.Id,
            EmployeeName = employee.Name,
            CurrentPositionName = positionName,
            Salary = employee.Salary,
            HireDate = employee.HireDate,
            IsBonusEligible = bonusCalculation.IsEligible,
            BonusAmount = bonusCalculation.BonusAmount,
            IneligibilityReason = bonusCalculation.IneligibilityReason
        };
    }
}
