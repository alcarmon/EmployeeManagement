namespace EmployeeManagement.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(PersistenceConstants.Tables.Users);

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedOnAdd();

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(DomainValidationConstants.UserEmailMaxLength);

        builder.Property(user => user.PasswordHash)
            .IsRequired()
            .HasMaxLength(DomainValidationConstants.UserPasswordHashMaxLength);

        builder.Property(user => user.Role)
            .IsRequired()
            .HasMaxLength(DomainValidationConstants.UserRoleMaxLength);

        builder.Property(user => user.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(user => user.CreatedAt)
            .IsRequired();

        builder.Property(user => user.LastLoginAt)
            .IsRequired(false);

        builder.Property(user => user.EmployeeId)
            .IsRequired(false);

        // Foreign key
        builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(user => user.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        // Unique index on email
        builder.HasIndex(user => user.Email)
            .IsUnique();

        // Indexes
        builder.HasIndex(user => user.IsActive);
        builder.HasIndex(user => user.Role);
    }
}
