namespace EmployeeManagement.Persistence.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly EmployeeManagementDbContext _context;

    public PositionRepository(EmployeeManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Position?> GetByIdAsync(int positionId)
    {
        return await _context.Positions
            .AsNoTracking()
            .FirstOrDefaultAsync(position => position.Id == positionId);
    }

    public async Task<Position?> GetByNameAsync(string name)
    {
        return await _context.Positions
            .AsNoTracking()
            .FirstOrDefaultAsync(position => position.Name == name);
    }

    public async Task<IReadOnlyCollection<Position>> GetAllAsync()
    {
        return await _context.Positions
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(int positionId)
    {
        return await _context.Positions
            .AnyAsync(position => position.Id == positionId);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Positions
            .AnyAsync(position => position.Name == name);
    }

    public async Task AddAsync(Position position)
    {
        await _context.Positions.AddAsync(position);
    }

    public async Task UpdateAsync(Position position)
    {
        _context.Positions.Update(position);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int positionId)
    {
        Position? position = await _context.Positions.FindAsync(positionId);
        if (position != null)
        {
            _context.Positions.Remove(position);
        }
    }
}
