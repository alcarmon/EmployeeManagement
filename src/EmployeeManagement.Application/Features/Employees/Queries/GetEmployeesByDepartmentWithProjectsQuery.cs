namespace EmployeeManagement.Application.Features.Employees.Queries;

public class GetEmployeesByDepartmentWithProjectsQuery : IRequest<IReadOnlyCollection<EmployeeResponse>>
{
    public int DepartmentId { get; set; }
}

public class GetEmployeesByDepartmentWithProjectsQueryHandler
    : IRequestHandler<GetEmployeesByDepartmentWithProjectsQuery, IReadOnlyCollection<EmployeeResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBonusStrategyFactory _bonusStrategyFactory;

    public GetEmployeesByDepartmentWithProjectsQueryHandler(
        IUnitOfWork unitOfWork,
        IBonusStrategyFactory bonusStrategyFactory)
    {
        _unitOfWork = unitOfWork;
        _bonusStrategyFactory = bonusStrategyFactory;
    }

    public async Task<IReadOnlyCollection<EmployeeResponse>> Handle(
        GetEmployeesByDepartmentWithProjectsQuery request,
        CancellationToken cancellationToken)
    {
        EmployeeManagement.Domain.Entities.Department? department = await _unitOfWork.Departments.GetByIdAsync(request.DepartmentId);
        if (department == null)
            throw new NotFoundException($"Department with ID {request.DepartmentId} was not found.");

        IReadOnlyCollection<EmployeeManagement.Domain.Entities.Employee> employees = await _unitOfWork.Employees.GetByDepartmentWithActiveProjectsAsync(request.DepartmentId);
        List<EmployeeResponse> responses = new List<EmployeeResponse>();

        foreach (EmployeeManagement.Domain.Entities.Employee employee in employees)
        {
            EmployeeManagement.Domain.Entities.Position? position = await _unitOfWork.Positions.GetByIdAsync(employee.CurrentPositionId);
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
                DepartmentName = department.Name,
                HireDate = employee.HireDate,
                IsBonusEligible = bonusCalculation.IsEligible,
                BonusAmount = bonusCalculation.BonusAmount,
                IsActive = employee.IsActive
            });
        }

        return responses.AsReadOnly();
    }
}
