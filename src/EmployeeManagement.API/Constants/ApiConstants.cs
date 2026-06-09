namespace EmployeeManagement.API.Constants;

public static class ApiConstants
{
    public const string SwaggerVersion = "v1";
    public const string SwaggerTitle = "Employee Management API";
    public const string BearerSecurityScheme = "Bearer";
    public const string AuthorizationHeader = "Authorization";
    public const string BearerScheme = "bearer";
    public const string JwtBearerFormat = "JWT";
    public const string SwaggerJsonEndpoint = "/swagger/v1/swagger.json";
    public const string SwaggerDisplayName = "Employee Management API v1";
    public const string SwaggerRoutePrefix = "";
    public const string SwaggerBearerDescription = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"";
    public const string AdminOrUserRoles = $"{Roles.Admin},{Roles.User}";
    public const string JsonContentType = "application/json";
    public const string RequestLogTemplate = "HTTP {Method} {Path}{QueryString} responded {StatusCode} in {ElapsedMilliseconds}ms";
    public const string UnhandledExceptionLogMessage = "Unhandled exception while processing request.";

    public static class Routes
    {
        public const string Employees = "api/[controller]";
        public const string Auth = "api/auth";
        public const string Id = "{id:int}";
        public const string DepartmentWithProjects = "department/{departmentId:int}/with-projects";

        public const string Register = "register";
        public const string Login = "login";
    }

    public static class JwtConfigurationKeys
    {
        public const string Issuer = "Jwt:Issuer";
        public const string Audience = "Jwt:Audience";
        public const string SecretKey = "Jwt:SecretKey";
    }
}
