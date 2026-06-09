namespace EmployeeManagement.Application.Common.Interfaces;

public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(int departmentId);
    Task<Department?> GetByNameAsync(string name);
    Task<IReadOnlyCollection<Department>> GetAllAsync();
    Task<bool> ExistsByIdAsync(int departmentId);
    Task AddAsync(Department department);
    Task UpdateAsync(Department department);
    Task DeleteAsync(int departmentId);
}
