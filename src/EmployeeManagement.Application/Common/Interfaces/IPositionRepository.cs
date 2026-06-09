namespace EmployeeManagement.Application.Common.Interfaces;

public interface IPositionRepository
{
    Task<Position?> GetByIdAsync(int positionId);
    Task<Position?> GetByNameAsync(string name);
    Task<IReadOnlyCollection<Position>> GetAllAsync();
    Task<bool> ExistsByIdAsync(int positionId);
    Task<bool> ExistsByNameAsync(string name);
    Task AddAsync(Position position);
    Task UpdateAsync(Position position);
    Task DeleteAsync(int positionId);
}
