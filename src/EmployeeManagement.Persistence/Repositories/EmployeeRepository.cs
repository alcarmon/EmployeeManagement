namespace EmployeeManagement.Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeManagementDbContext _context;

    public EmployeeRepository(EmployeeManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetByIdAsync(int employeeId)
    {
        return await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(employee => employee.Id == employeeId);
    }

    public async Task<Employee?> GetByIdWithDetailsAsync(int employeeId)
    {
        return await _context.Employees
            .AsNoTracking()
            .Include(employee => employee.PositionHistory)
            .Include(employee => employee.Projects)
            .FirstOrDefaultAsync(employee => employee.Id == employeeId);
    }

    public async Task<IReadOnlyCollection<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Employee>> GetByDepartmentIdAsync(int departmentId)
    {
        return await _context.Employees
            .AsNoTracking()
            .Where(employee => employee.DepartmentId == departmentId)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Employee>> GetByDepartmentWithActiveProjectsAsync(int departmentId)
    {
        return await _context.Employees
            .AsNoTracking()
            .Include(employee => employee.Projects)
            .Where(employee => employee.DepartmentId == departmentId && employee.Projects.Any(project => project.UnassignedDate == null))
            .ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(int employeeId)
    {
        return await _context.Employees
            .AnyAsync(employee => employee.Id == employeeId);
    }

    public async Task<bool> ExistsByIdentificationNumberAsync(string identificationNumber)
    {
        return await _context.Employees
            .AnyAsync(employee => employee.IdentificationNumber == identificationNumber);
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int employeeId)
    {
        Employee? employee = await _context.Employees.FindAsync(employeeId);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
        }
    }
}
