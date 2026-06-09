namespace EmployeeManagement.Application;

public interface IEmployeeService
{
    Task<IReadOnlyCollection<EmployeeResponse>> GetAllEmployeesAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<EmployeeResponse>> GetEmployeesByDepartmentWithProjectsAsync(int departmentId, CancellationToken cancellationToken);
    Task<EmployeeDetailsResponse> GetEmployeeByIdAsync(int employeeId, CancellationToken cancellationToken);
    Task<EmployeeBonusResponse> GetEmployeeBonusAsync(int employeeId, CancellationToken cancellationToken);
    Task<int> CreateEmployeeAsync(CreateEmployeeRequest request, CancellationToken cancellationToken);
    Task UpdateEmployeeAsync(int employeeId, UpdateEmployeeRequest request, CancellationToken cancellationToken);
    Task DeleteEmployeeAsync(int employeeId, CancellationToken cancellationToken);
}
