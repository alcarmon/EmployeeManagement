namespace EmployeeManagement.Application.Common.Interfaces;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(int projectId);
    Task<Project?> GetByNameAsync(string name);
    Task<IReadOnlyCollection<Project>> GetAllAsync();
    Task<bool> ExistsByIdAsync(int projectId);
    Task AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(int projectId);
}
