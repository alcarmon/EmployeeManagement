namespace EmployeeManagement.Persistence;

public sealed class EmployeeManagementDbContextFactory
    : IDesignTimeDbContextFactory<EmployeeManagementDbContext>
{
    public EmployeeManagementDbContext CreateDbContext(
        string[] args)
    {
        DbContextOptionsBuilder<EmployeeManagementDbContext> optionsBuilder =
            new DbContextOptionsBuilder<EmployeeManagementDbContext>();

        optionsBuilder.UseSqlServer(PersistenceConstants.DesignTimeConnectionString);

        return new EmployeeManagementDbContext(optionsBuilder.Options);
    }
}
