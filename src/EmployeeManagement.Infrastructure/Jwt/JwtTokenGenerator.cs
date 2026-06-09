namespace EmployeeManagement.Infrastructure.Jwt;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expirationMinutes;

    public JwtTokenGenerator(
        string secretKey,
        string issuer,
        string audience,
        int expirationMinutes = InfrastructureConstants.DefaultJwtExpirationMinutes)
    {
        if (string.IsNullOrWhiteSpace(secretKey))
            throw new ArgumentException("Secret key cannot be empty.", nameof(secretKey));

        if (secretKey.Length < DomainValidationConstants.JwtSecretKeyMinLength)
            throw new ArgumentException($"Secret key must be at least {DomainValidationConstants.JwtSecretKeyMinLength} characters long.", nameof(secretKey));

        _secretKey = secretKey;
        _issuer = issuer ?? InfrastructureConstants.DefaultIssuer;
        _audience = audience ?? InfrastructureConstants.DefaultAudience;
        _expirationMinutes = expirationMinutes;
    }

    public string GenerateToken(int userId, string email, string role)
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(InfrastructureConstants.SubjectClaimName, userId.ToString()),
            new Claim(InfrastructureConstants.IssuedAtClaimName, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
