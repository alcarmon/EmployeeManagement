namespace EmployeeManagement.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly EmployeeManagementDbContext _context;
    private IDbContextTransaction? _transaction;

    private IEmployeeRepository? _employeeRepository;
    private IPositionRepository? _positionRepository;
    private IDepartmentRepository? _departmentRepository;
    private IProjectRepository? _projectRepository;
    private IUserRepository? _userRepository;

    public UnitOfWork(EmployeeManagementDbContext context)
    {
        _context = context;
    }

    public IEmployeeRepository Employees
        => _employeeRepository ??= new EmployeeRepository(_context);

    public IPositionRepository Positions
        => _positionRepository ??= new PositionRepository(_context);

    public IDepartmentRepository Departments
        => _departmentRepository ??= new DepartmentRepository(_context);

    public IProjectRepository Projects
        => _projectRepository ??= new ProjectRepository(_context);

    public IUserRepository Users
        => _userRepository ??= new UserRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
        return true;
    }

    public async Task<bool> CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction?.CommitAsync()!;
            return true;
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public async Task<bool> RollbackTransactionAsync()
    {
        try
        {
            await _transaction?.RollbackAsync()!;
            return true;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context?.Dispose();
    }
}
