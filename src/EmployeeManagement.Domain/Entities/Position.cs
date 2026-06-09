namespace EmployeeManagement.Domain.Entities;

public class Position
{
    public int Id { get; internal set; }
    public string Name { get; private set; } = string.Empty;

    private Position() { }

    public Position(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Position name cannot be empty.", nameof(name));

        Name = name;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Position name cannot be empty.", nameof(newName));

        Name = newName;
    }
}
