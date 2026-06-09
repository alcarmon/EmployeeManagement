namespace EmployeeManagement.Application.Features.Employees.Commands;

public class DeleteEmployeeCommand : IRequest<bool>
{
    public int EmployeeId { get; set; }
}

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        bool employeeExists = await _unitOfWork.Employees.ExistsByIdAsync(request.EmployeeId);
        if (!employeeExists)
            throw new NotFoundException($"Employee with ID {request.EmployeeId} was not found.");

        await _unitOfWork.Employees.DeleteAsync(request.EmployeeId);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
