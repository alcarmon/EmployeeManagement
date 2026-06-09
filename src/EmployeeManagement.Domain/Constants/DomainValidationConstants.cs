namespace EmployeeManagement.Domain.Constants;

public static class DomainValidationConstants
{
    public const int MinimumEntityId = 1;
    public const int MinimumTextLength = 2;
    public const int EmployeeIdentificationNumberMaxLength = 50;
    public const int EmployeeNameMaxLength = 100;
    public const int DepartmentNameMaxLength = 100;
    public const int DepartmentDescriptionMaxLength = 500;
    public const int PositionNameMaxLength = 50;
    public const int ProjectNameMaxLength = 150;
    public const int ProjectDescriptionMaxLength = 1000;
    public const int UserEmailMaxLength = 256;
    public const int UserPasswordHashMaxLength = 500;
    public const int UserRoleMaxLength = 50;
    public const int PasswordMinLength = 6;
    public const int JwtSecretKeyMinLength = 32;
    public const decimal MinimumSalary = 0m;
    public const string PasswordUppercasePattern = "[A-Z]";
    public const string PasswordDigitPattern = "[0-9]";
}
