namespace EmployeeManagement.Application.Features.Employees.Queries;

public class GetAllEmployeesQuery : IRequest<IReadOnlyCollection<EmployeeResponse>>
{
}

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IReadOnlyCollection<EmployeeResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBonusStrategyFactory _bonusStrategyFactory;

    public GetAllEmployeesQueryHandler(
        IUnitOfWork unitOfWork,
        IBonusStrategyFactory bonusStrategyFactory)
    {
        _unitOfWork = unitOfWork;
        _bonusStrategyFactory = bonusStrategyFactory;
    }

    public async Task<IReadOnlyCollection<EmployeeResponse>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<EmployeeManagement.Domain.Entities.Employee> employees = await _unitOfWork.Employees.GetAllAsync();

        List<EmployeeResponse> responses = new List<EmployeeResponse>();

        foreach (EmployeeManagement.Domain.Entities.Employee employee in employees)
        {
            EmployeeManagement.Domain.Entities.Position? position = await _unitOfWork.Positions.GetByIdAsync(employee.CurrentPositionId);
            EmployeeManagement.Domain.Entities.Department? department = await _unitOfWork.Departments.GetByIdAsync(employee.DepartmentId);
            string positionName = position?.Name ?? ApplicationConstants.UnknownValue;
            EmployeeBonusCalculation bonusCalculation = EmployeeBonusCalculator.Calculate(
                employee,
                positionName,
                _bonusStrategyFactory,
                DateTime.UtcNow);

            responses.Add(new EmployeeResponse
            {
                Id = employee.Id,
                IdentificationNumber = employee.IdentificationNumber,
                Name = employee.Name,
                Salary = employee.Salary,
                CurrentPositionId = employee.CurrentPositionId,
                CurrentPositionName = positionName,
                DepartmentId = employee.DepartmentId,
                DepartmentName = department?.Name ?? ApplicationConstants.UnknownValue,
                HireDate = employee.HireDate,
                IsBonusEligible = bonusCalculation.IsEligible,
                BonusAmount = bonusCalculation.BonusAmount,
                IsActive = employee.IsActive
            });
        }

        return responses.AsReadOnly();
    }
}
