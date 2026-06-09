namespace EmployeeManagement.Persistence.Configurations;

public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
{
    public void Configure(EntityTypeBuilder<EmployeeProject> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.EmployeeProjects);

        builder.HasKey(employeeProject => employeeProject.Id);

        builder.Property(employeeProject => employeeProject.Id)
            .ValueGeneratedOnAdd();

        builder.Property(employeeProject => employeeProject.EmployeeId)
            .IsRequired();

        builder.Property(employeeProject => employeeProject.ProjectId)
            .IsRequired();

        builder.Property(employeeProject => employeeProject.AssignedDate)
            .IsRequired();

        builder.Property(employeeProject => employeeProject.UnassignedDate)
            .IsRequired(false);

        // Foreign keys
        builder.HasOne<Employee>()
            .WithMany(employee => employee.Projects)
            .HasForeignKey(employeeProject => employeeProject.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Project>()
            .WithMany(project => project.EmployeeProjects)
            .HasForeignKey(employeeProject => employeeProject.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // Unique constraint - Employee can only be assigned to a project once
        builder.HasIndex(employeeProject => new { employeeProject.EmployeeId, employeeProject.ProjectId })
            .IsUnique();

        // Indexes
        builder.HasIndex(employeeProject => employeeProject.EmployeeId);
        builder.HasIndex(employeeProject => employeeProject.ProjectId);
        builder.HasIndex(employeeProject => employeeProject.AssignedDate);
    }
}
