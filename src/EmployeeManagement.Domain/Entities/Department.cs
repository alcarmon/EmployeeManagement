namespace EmployeeManagement.Domain.Entities;

public class Department
{
    public int Id { get; internal set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    private readonly List<Employee> _employees = new();
    public IReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();

    private Department() { }

    public Department(string name, string description = "")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Department name cannot be empty.", nameof(name));

        Name = name;
        Description = description;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Department name cannot be empty.", nameof(newName));

        Name = newName;
    }

    public void UpdateDescription(string description)
    {
        Description = description ?? string.Empty;
    }

    public void AddEmployee(Employee employee)
    {
        if (_employees.Contains(employee))
            return;

        _employees.Add(employee);
    }

    public void RemoveEmployee(Employee employee)
    {
        _employees.Remove(employee);
    }
}
