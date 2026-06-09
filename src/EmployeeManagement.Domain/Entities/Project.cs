namespace EmployeeManagement.Domain.Entities;

public class Project
{
    public int Id { get; internal set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    private readonly List<EmployeeProject> _employeeProjects = new();
    public IReadOnlyCollection<EmployeeProject> EmployeeProjects => _employeeProjects.AsReadOnly();

    private Project() { }

    public Project(string name, string description, DateTime startDate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Project name cannot be empty.", nameof(name));

        Name = name;
        Description = description ?? string.Empty;
        StartDate = startDate;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Project name cannot be empty.", nameof(newName));

        Name = newName;
    }

    public void UpdateDescription(string description)
    {
        Description = description ?? string.Empty;
    }

    public void SetEndDate(DateTime endDate)
    {
        if (endDate < StartDate)
            throw new ArgumentException("End date cannot be before start date.", nameof(endDate));

        EndDate = endDate;
    }

    public void AssignEmployee(EmployeeProject employeeProject)
    {
        if (_employeeProjects.Any(ep => ep.EmployeeId == employeeProject.EmployeeId))
            return;

        _employeeProjects.Add(employeeProject);
    }

    public void UnassignEmployee(int employeeId)
    {
        EmployeeProject? employeeProject = _employeeProjects.FirstOrDefault(ep => ep.EmployeeId == employeeId);
        if (employeeProject != null)
            _employeeProjects.Remove(employeeProject);
    }
}
