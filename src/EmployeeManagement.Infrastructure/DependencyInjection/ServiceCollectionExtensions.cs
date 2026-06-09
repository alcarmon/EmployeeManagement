namespace EmployeeManagement.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Get JWT settings from configuration
        string secretKey = configuration[InfrastructureConstants.JwtConfigurationKeys.SecretKey]
            ?? throw new InvalidOperationException($"{InfrastructureConstants.JwtConfigurationKeys.SecretKey} is not configured");
        string issuer = configuration[InfrastructureConstants.JwtConfigurationKeys.Issuer] ?? InfrastructureConstants.DefaultIssuer;
        string audience = configuration[InfrastructureConstants.JwtConfigurationKeys.Audience] ?? InfrastructureConstants.DefaultAudience;
        string expirationMinutesString = configuration[InfrastructureConstants.JwtConfigurationKeys.ExpirationMinutes]
            ?? InfrastructureConstants.DefaultJwtExpirationMinutes.ToString();
        int expirationMinutes = int.TryParse(expirationMinutesString, out int minutes)
            ? minutes
            : InfrastructureConstants.DefaultJwtExpirationMinutes;

        // Register JWT Token Generator
        services.AddSingleton<IJwtTokenGenerator>(
            new JwtTokenGenerator(secretKey, issuer, audience, expirationMinutes)
        );

        // Register Password Hasher
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();

        // Register Bonus Strategy Factory
        services.AddSingleton<IBonusStrategyFactory, BonusStrategyFactory>();

        return services;
    }
}
