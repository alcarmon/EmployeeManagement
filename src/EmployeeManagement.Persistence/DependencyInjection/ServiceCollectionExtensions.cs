namespace EmployeeManagement.Persistence.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString(PersistenceConstants.DefaultConnectionName);

        services.AddDbContext<EmployeeManagementDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
