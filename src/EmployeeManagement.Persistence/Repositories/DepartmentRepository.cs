namespace EmployeeManagement.Persistence.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly EmployeeManagementDbContext _context;

    public DepartmentRepository(EmployeeManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Department?> GetByIdAsync(int departmentId)
    {
        return await _context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(department => department.Id == departmentId);
    }

    public async Task<Department?> GetByNameAsync(string name)
    {
        return await _context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(department => department.Name == name);
    }

    public async Task<IReadOnlyCollection<Department>> GetAllAsync()
    {
        return await _context.Departments
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(int departmentId)
    {
        return await _context.Departments
            .AnyAsync(department => department.Id == departmentId);
    }

    public async Task AddAsync(Department department)
    {
        await _context.Departments.AddAsync(department);
    }

    public async Task UpdateAsync(Department department)
    {
        _context.Departments.Update(department);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int departmentId)
    {
        Department? department = await _context.Departments.FindAsync(departmentId);
        if (department != null)
        {
            _context.Departments.Remove(department);
        }
    }
}
