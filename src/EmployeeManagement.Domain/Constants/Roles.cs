namespace EmployeeManagement.Domain.Constants;

public static class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";

    public static readonly List<string> AllRoles = new() { Admin, User };

    public static bool IsValid(string role) => AllRoles.Contains(role);
}
