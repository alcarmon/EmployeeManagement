namespace EmployeeManagement.Infrastructure.Constants;

public static class InfrastructureConstants
{
    public const string DefaultIssuer = "EmployeeManagement";
    public const string DefaultAudience = "EmployeeManagementAPI";
    public const int DefaultJwtExpirationMinutes = 60;
    public const string SubjectClaimName = "sub";
    public const string IssuedAtClaimName = "iat";

    public static class JwtConfigurationKeys
    {
        public const string SecretKey = "Jwt:SecretKey";
        public const string Issuer = "Jwt:Issuer";
        public const string Audience = "Jwt:Audience";
        public const string ExpirationMinutes = "Jwt:ExpirationMinutes";
    }
}
