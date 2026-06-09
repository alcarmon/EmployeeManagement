namespace EmployeeManagement.Application.Features.Employees.Queries;

using EmployeeManagement.Domain.Entities;

public class GetEmployeeByIdQuery : IRequest<EmployeeDetailsResponse>
{
    public int EmployeeId { get; set; }
}

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDetailsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBonusStrategyFactory _bonusStrategyFactory;

    public GetEmployeeByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IBonusStrategyFactory bonusStrategyFactory)
    {
        _unitOfWork = unitOfWork;
        _bonusStrategyFactory = bonusStrategyFactory;
    }

    public async Task<EmployeeDetailsResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        Employee? employee = await _unitOfWork.Employees.GetByIdWithDetailsAsync(request.EmployeeId) ?? throw new NotFoundException($"Employee with ID {request.EmployeeId} was not found.");

        Position? position = await _unitOfWork.Positions.GetByIdAsync(employee.CurrentPositionId);
        Department? department = await _unitOfWork.Departments.GetByIdAsync(employee.DepartmentId);
        string positionName = position?.Name ?? ApplicationConstants.UnknownValue;
        EmployeeBonusCalculation bonusCalculation = EmployeeBonusCalculator.Calculate(
            employee,
            positionName,
            _bonusStrategyFactory,
            DateTime.UtcNow);

        EmployeeDetailsResponse response = new EmployeeDetailsResponse
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
            BonusIneligibilityReason = bonusCalculation.IneligibilityReason,
            IsActive = employee.IsActive
        };

        foreach (PositionHistory history in employee.PositionHistory.OrderBy(h => h.StartDate))
        {
            Position? historyPosition = await _unitOfWork.Positions.GetByIdAsync(history.PositionId);

            response.PositionHistory.Add(new EmployeeDetailsResponse.PositionHistoryDto
            {
                Id = history.Id,
                PositionId = history.PositionId,
                PositionName = historyPosition?.Name ?? ApplicationConstants.UnknownValue,
                StartDate = history.StartDate,
                EndDate = history.EndDate
            });
        }

        foreach (EmployeeProject employeeProject in employee.Projects.OrderBy(p => p.AssignedDate))
        {
            Project? project = await _unitOfWork.Projects.GetByIdAsync(employeeProject.ProjectId);

            response.Projects.Add(new EmployeeDetailsResponse.ProjectDto
            {
                ProjectId = employeeProject.ProjectId,
                ProjectName = project?.Name ?? ApplicationConstants.UnknownValue,
                AssignedDate = employeeProject.AssignedDate,
                UnassignedDate = employeeProject.UnassignedDate
            });
        }

        return response;
    }
}
