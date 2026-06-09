namespace EmployeeManagement.Persistence.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.Positions);

        builder.HasKey(position => position.Id);

        builder.Property(position => position.Id)
            .ValueGeneratedOnAdd();

        builder.Property(position => position.Name)
            .IsRequired()
            .HasMaxLength(DomainValidationConstants.PositionNameMaxLength);

        // Unique index
        builder.HasIndex(position => position.Name)
            .IsUnique();
    }
}
