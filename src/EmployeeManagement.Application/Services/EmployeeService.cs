namespace EmployeeManagement.Application.Services;

public sealed class EmployeeService : IEmployeeService
{
    private readonly IMediator _mediator;

    public EmployeeService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IReadOnlyCollection<EmployeeResponse>> GetAllEmployeesAsync(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllEmployeesQuery(), cancellationToken);
    }

    public async Task<IReadOnlyCollection<EmployeeResponse>> GetEmployeesByDepartmentWithProjectsAsync(int departmentId, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetEmployeesByDepartmentWithProjectsQuery { DepartmentId = departmentId }, cancellationToken);
    }

    public async Task<EmployeeDetailsResponse> GetEmployeeByIdAsync(int employeeId, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetEmployeeByIdQuery { EmployeeId = employeeId }, cancellationToken);
    }

    public async Task<EmployeeBonusResponse> GetEmployeeBonusAsync(int employeeId, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetEmployeeBonusQuery { EmployeeId = employeeId }, cancellationToken);
    }

    public async Task<int> CreateEmployeeAsync(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        CreateEmployeeCommand command = new CreateEmployeeCommand
        {
            IdentificationNumber = request.IdentificationNumber,
            Name = request.Name,
            Salary = request.Salary,
            CurrentPositionId = request.CurrentPositionId,
            DepartmentId = request.DepartmentId,
            HireDate = request.HireDate
        };

        return await _mediator.Send(command, cancellationToken);
    }

    public async Task UpdateEmployeeAsync(int employeeId, UpdateEmployeeRequest request, CancellationToken cancellationToken)
    {
        UpdateEmployeeCommand command = new UpdateEmployeeCommand
        {
            EmployeeId = employeeId,
            IdentificationNumber = request.IdentificationNumber,
            Name = request.Name,
            Salary = request.Salary,
            PositionId = request.PositionId,
            DepartmentId = request.DepartmentId,
            ProjectIds = request.ProjectIds
        };

        await _mediator.Send(command, cancellationToken);
    }

    public async Task DeleteEmployeeAsync(int employeeId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteEmployeeCommand { EmployeeId = employeeId }, cancellationToken);
    }
}
