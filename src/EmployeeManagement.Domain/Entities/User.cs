namespace EmployeeManagement.Domain.Entities;

public class User
{
    public int Id { get; internal set; }
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public int? EmployeeId { get; private set; }
    public string Role { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    private User() { }

    public User(string email, string passwordHash, string role, int? employeeId = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));

        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role cannot be empty.", nameof(role));

        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        EmployeeId = employeeId;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            throw new ArgumentException("Email cannot be empty.", nameof(newEmail));

        Email = newEmail;
    }

    public void UpdatePasswordHash(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password hash cannot be empty.", nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
    }

    public void UpdateRole(string newRole)
    {
        if (string.IsNullOrWhiteSpace(newRole))
            throw new ArgumentException("Role cannot be empty.", nameof(newRole));

        Role = newRole;
    }

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;

    public void RecordLogin() => LastLoginAt = DateTime.UtcNow;
}
