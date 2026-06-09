namespace EmployeeManagement.Application.Features.Employees.Commands;

public class CreateEmployeeCommand : IRequest<int>
{
    public string IdentificationNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public int CurrentPositionId { get; set; }
    public int DepartmentId { get; set; }
    public DateTime HireDate { get; set; }
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        bool identificationNumberExists = await _unitOfWork.Employees.ExistsByIdentificationNumberAsync(request.IdentificationNumber);
        if (identificationNumberExists)
            throw new InvalidOperationException($"Employee with identification number {request.IdentificationNumber} already exists.");

        bool positionExists = await _unitOfWork.Positions.ExistsByIdAsync(request.CurrentPositionId);
        if (!positionExists)
            throw new KeyNotFoundException($"Position with ID {request.CurrentPositionId} not found.");

        bool departmentExists = await _unitOfWork.Departments.ExistsByIdAsync(request.DepartmentId);
        if (!departmentExists)
            throw new KeyNotFoundException($"Department with ID {request.DepartmentId} not found.");

        EmployeeManagement.Domain.Entities.Employee employee = new EmployeeManagement.Domain.Entities.Employee(
            request.IdentificationNumber,
            request.Name,
            request.Salary,
            request.CurrentPositionId,
            request.DepartmentId,
            request.HireDate
        );

        await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return employee.Id;
    }
}
