namespace EmployeeManagement.Infrastructure.Security;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hash)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Hash cannot be empty.", nameof(hash));

        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
