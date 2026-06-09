namespace EmployeeManagement.Persistence.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.Departments);

        builder.HasKey(department => department.Id);

        builder.Property(department => department.Id)
            .ValueGeneratedOnAdd();

        builder.Property(department => department.Name)
            .IsRequired()
            .HasMaxLength(DomainValidationConstants.DepartmentNameMaxLength);

        builder.Property(department => department.Description)
            .HasMaxLength(DomainValidationConstants.DepartmentDescriptionMaxLength);

        // Unique index
        builder.HasIndex(department => department.Name)
            .IsUnique();
    }
}
