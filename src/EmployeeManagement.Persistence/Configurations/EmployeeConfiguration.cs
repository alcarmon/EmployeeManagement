namespace EmployeeManagement.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.Employees);

        builder.HasKey(employee => employee.Id);

        builder.Property(employee => employee.Id)
            .ValueGeneratedOnAdd();

        builder.Property(employee => employee.IdentificationNumber)
            .IsRequired()
            .HasMaxLength(DomainValidationConstants.EmployeeIdentificationNumberMaxLength);

        builder.Property(employee => employee.Name)
            .IsRequired()
            .HasMaxLength(DomainValidationConstants.EmployeeNameMaxLength);

        builder.Property(employee => employee.Salary)
            .HasPrecision(PersistenceConstants.SalaryPrecision, PersistenceConstants.SalaryScale)
            .IsRequired();

        builder.Property(employee => employee.CurrentPositionId)
            .IsRequired();

        builder.Property(employee => employee.DepartmentId)
            .IsRequired();

        builder.Property(employee => employee.HireDate)
            .IsRequired();

        builder.Property(employee => employee.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Relationships
        builder.HasOne<Position>()
            .WithMany()
            .HasForeignKey(employee => employee.CurrentPositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Department>()
            .WithMany(department => department.Employees)
            .HasForeignKey(employee => employee.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(employee => employee.IdentificationNumber)
            .IsUnique();

        builder.HasIndex(employee => employee.Name);
        builder.HasIndex(employee => employee.IsActive);
        builder.HasIndex(employee => employee.CurrentPositionId);
        builder.HasIndex(employee => employee.DepartmentId);
    }
}
