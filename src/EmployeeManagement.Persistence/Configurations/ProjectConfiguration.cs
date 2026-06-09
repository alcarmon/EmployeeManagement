namespace EmployeeManagement.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.Projects);

        builder.HasKey(project => project.Id);

        builder.Property(project => project.Id)
            .ValueGeneratedOnAdd();

        builder.Property(project => project.Name)
            .IsRequired()
            .HasMaxLength(DomainValidationConstants.ProjectNameMaxLength);

        builder.Property(project => project.Description)
            .HasMaxLength(DomainValidationConstants.ProjectDescriptionMaxLength);

        builder.Property(project => project.StartDate)
            .IsRequired();

        builder.Property(project => project.EndDate)
            .IsRequired(false);

        // Unique index
        builder.HasIndex(project => project.Name)
            .IsUnique();

        // Indexes
        builder.HasIndex(project => project.StartDate);
        builder.HasIndex(project => project.EndDate);
    }
}
