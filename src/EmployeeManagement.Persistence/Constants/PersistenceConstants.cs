namespace EmployeeManagement.Persistence.Constants;

public static class PersistenceConstants
{
    public const string DefaultConnectionName = "DefaultConnection";
    public const int SalaryPrecision = 18;
    public const int SalaryScale = 2;
    public const string DesignTimeConnectionString = "Server=ALBER_PC\\SQLEXPRESS;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

    public static class Tables
    {
        public const string Departments = "Departments";
        public const string Employees = "Employees";
        public const string EmployeeProjects = "EmployeeProjects";
        public const string PositionHistories = "PositionHistories";
        public const string Positions = "Positions";
        public const string Projects = "Projects";
        public const string Users = "Users";
    }

    public static class SeedData
    {
        public const int EmployeePositionId = 1;
        public const int ManagerPositionId = 2;
        public const int SeniorManagerPositionId = 3;
        public const int DirectorPositionId = 4;
        public const int EngineeringDepartmentId = 1;
        public const int SalesDepartmentId = 2;
        public const int HumanResourcesDepartmentId = 3;
        public const int FinanceDepartmentId = 4;
        public const int AdminUserId = 1;
        public const string EngineeringDepartmentName = "Engineering";
        public const string EngineeringDepartmentDescription = "Software development and infrastructure";
        public const string SalesDepartmentName = "Sales";
        public const string SalesDepartmentDescription = "Commercial and business development";
        public const string HumanResourcesDepartmentName = "Human Resources";
        public const string HumanResourcesDepartmentDescription = "People management and recruitment";
        public const string FinanceDepartmentName = "Finance";
        public const string FinanceDepartmentDescription = "Financial management and accounting";
        public const string AdminEmail = "admin@employeemanagement.local";
        public const string AdminPasswordHash = "$2a$11$B8rPrvToa6xUe8HG1h8EPeM/OVCoJyobrHvbxDCix6jpE3AAF8C8i";
        public const int AdminCreatedAtYear = 2026;
        public const int AdminCreatedAtMonth = 6;
        public const int AdminCreatedAtDay = 7;
    }
}
