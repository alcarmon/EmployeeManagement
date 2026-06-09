namespace EmployeeManagement.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly EmployeeManagementDbContext _context;

    public UserRepository(EmployeeManagementDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == userId);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users
            .AnyAsync(user => user.Email == email);
    }

    public async Task<IReadOnlyCollection<User>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int userId)
    {
        User? user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
        }
    }
}
