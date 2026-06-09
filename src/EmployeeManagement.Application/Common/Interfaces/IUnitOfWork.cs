namespace EmployeeManagement.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IEmployeeRepository Employees { get; }
    IPositionRepository Positions { get; }
    IDepartmentRepository Departments { get; }
    IProjectRepository Projects { get; }
    IUserRepository Users { get; }

    Task<int> SaveChangesAsync();
    Task<bool> BeginTransactionAsync();
    Task<bool> CommitTransactionAsync();
    Task<bool> RollbackTransactionAsync();
}
