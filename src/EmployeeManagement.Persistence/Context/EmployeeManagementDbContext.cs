namespace EmployeeManagement.Persistence.Context;

public class EmployeeManagementDbContext : DbContext
{
    public EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<PositionHistory> PositionHistories { get; set; } = null!;
    public DbSet<EmployeeProject> EmployeeProjects { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new PositionConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new PositionHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeProjectConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        // Seed data
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Positions
        modelBuilder.Entity<Position>().HasData(
            new { Id = PersistenceConstants.SeedData.EmployeePositionId, Name = PositionNames.Employee },
            new { Id = PersistenceConstants.SeedData.ManagerPositionId, Name = PositionNames.Manager },
            new { Id = PersistenceConstants.SeedData.SeniorManagerPositionId, Name = PositionNames.SeniorManager },
            new { Id = PersistenceConstants.SeedData.DirectorPositionId, Name = PositionNames.Director }
        );

        // Seed Departments
        modelBuilder.Entity<Department>().HasData(
            new { Id = PersistenceConstants.SeedData.EngineeringDepartmentId, Name = PersistenceConstants.SeedData.EngineeringDepartmentName, Description = PersistenceConstants.SeedData.EngineeringDepartmentDescription },
            new { Id = PersistenceConstants.SeedData.SalesDepartmentId, Name = PersistenceConstants.SeedData.SalesDepartmentName, Description = PersistenceConstants.SeedData.SalesDepartmentDescription },
            new { Id = PersistenceConstants.SeedData.HumanResourcesDepartmentId, Name = PersistenceConstants.SeedData.HumanResourcesDepartmentName, Description = PersistenceConstants.SeedData.HumanResourcesDepartmentDescription },
            new { Id = PersistenceConstants.SeedData.FinanceDepartmentId, Name = PersistenceConstants.SeedData.FinanceDepartmentName, Description = PersistenceConstants.SeedData.FinanceDepartmentDescription }
        );

        modelBuilder.Entity<User>().HasData(
            new
            {
                Id = PersistenceConstants.SeedData.AdminUserId,
                Email = PersistenceConstants.SeedData.AdminEmail,
                PasswordHash = PersistenceConstants.SeedData.AdminPasswordHash,
                EmployeeId = (int?)null,
                Role = Roles.Admin,
                IsActive = true,
                CreatedAt = new DateTime(
                    PersistenceConstants.SeedData.AdminCreatedAtYear,
                    PersistenceConstants.SeedData.AdminCreatedAtMonth,
                    PersistenceConstants.SeedData.AdminCreatedAtDay,
                    default,
                    default,
                    default,
                    DateTimeKind.Utc),
                LastLoginAt = (DateTime?)null
            }
        );
    }
}
