namespace EmployeeManagement.Persistence.Configurations;

public class PositionHistoryConfiguration : IEntityTypeConfiguration<PositionHistory>
{
    public void Configure(EntityTypeBuilder<PositionHistory> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.PositionHistories);

        builder.HasKey(positionHistory => positionHistory.Id);

        builder.Property(positionHistory => positionHistory.Id)
            .ValueGeneratedOnAdd();

        builder.Property(positionHistory => positionHistory.EmployeeId)
            .IsRequired();

        builder.Property(positionHistory => positionHistory.PositionId)
            .IsRequired();

        builder.Property(positionHistory => positionHistory.StartDate)
            .IsRequired();

        builder.Property(positionHistory => positionHistory.EndDate)
            .IsRequired(false);

        // Foreign keys
        builder.HasOne<Employee>()
            .WithMany(employee => employee.PositionHistory)
            .HasForeignKey(positionHistory => positionHistory.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Position>()
            .WithMany()
            .HasForeignKey(positionHistory => positionHistory.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(positionHistory => positionHistory.EmployeeId);
        builder.HasIndex(positionHistory => positionHistory.PositionId);
        builder.HasIndex(positionHistory => positionHistory.StartDate);
    }
}
