namespace EmployeeManagement.Application.Common.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(int employeeId);
    Task<Employee?> GetByIdWithDetailsAsync(int employeeId);
    Task<IReadOnlyCollection<Employee>> GetAllAsync();
    Task<IReadOnlyCollection<Employee>> GetByDepartmentIdAsync(int departmentId);
    Task<IReadOnlyCollection<Employee>> GetByDepartmentWithActiveProjectsAsync(int departmentId);
    Task<bool> ExistsByIdAsync(int employeeId);
    Task<bool> ExistsByIdentificationNumberAsync(string identificationNumber);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(int employeeId);
}
