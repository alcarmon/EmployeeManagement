namespace EmployeeManagement.Application.Features.Employees.Commands;

public class UpdateEmployeeCommand : IRequest<bool>
{
    public int EmployeeId { get; set; }
    public string? IdentificationNumber { get; set; }
    public string? Name { get; set; }
    public decimal? Salary { get; set; }
    public int? PositionId { get; set; }
    public int? DepartmentId { get; set; }
    public List<int>? ProjectIds { get; set; }
}

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        EmployeeManagement.Domain.Entities.Employee? employee = await _unitOfWork.Employees.GetByIdWithDetailsAsync(request.EmployeeId);
        if (employee == null)
            throw new NotFoundException($"Employee with ID {request.EmployeeId} was not found.");

        if (!string.IsNullOrWhiteSpace(request.IdentificationNumber) &&
            !string.Equals(employee.IdentificationNumber, request.IdentificationNumber, StringComparison.OrdinalIgnoreCase))
        {
            bool identificationNumberExists = await _unitOfWork.Employees.ExistsByIdentificationNumberAsync(request.IdentificationNumber);
            if (identificationNumberExists)
                throw new InvalidOperationException($"Employee with identification number {request.IdentificationNumber} already exists.");

            employee.UpdateIdentificationNumber(request.IdentificationNumber);
        }

        if (!string.IsNullOrEmpty(request.Name))
            employee.UpdateName(request.Name);

        if (request.Salary.HasValue)
            employee.UpdateSalary(request.Salary.Value);

        if (request.PositionId.HasValue)
        {
            bool positionExists = await _unitOfWork.Positions.ExistsByIdAsync(request.PositionId.Value);
            if (!positionExists)
                throw new NotFoundException($"Position with ID {request.PositionId.Value} was not found.");

            employee.ChangePosition(request.PositionId.Value);
        }

        if (request.DepartmentId.HasValue)
        {
            bool departmentExists = await _unitOfWork.Departments.ExistsByIdAsync(request.DepartmentId.Value);
            if (!departmentExists)
                throw new NotFoundException($"Department with ID {request.DepartmentId.Value} was not found.");

            employee.ChangeDepartment(request.DepartmentId.Value);
        }

        if (request.ProjectIds != null)
        {
            List<int> requestedProjectIds = request.ProjectIds.Distinct().ToList();

            foreach (int projectId in requestedProjectIds)
            {
                bool projectExists = await _unitOfWork.Projects.ExistsByIdAsync(projectId);
                if (!projectExists)
                    throw new NotFoundException($"Project with ID {projectId} was not found.");
            }

            employee.UpdateProjectAssignments(requestedProjectIds, DateTime.UtcNow);
        }

        await _unitOfWork.Employees.UpdateAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
