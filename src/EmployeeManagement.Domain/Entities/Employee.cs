namespace EmployeeManagement.Domain.Entities;

public class Employee
{
    private readonly List<PositionHistory> _positionHistory = new();

    public int Id { get; internal set; }
    public string IdentificationNumber { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public decimal Salary { get; private set; }
    public int CurrentPositionId { get; private set; }
    public int DepartmentId { get; private set; }
    public DateTime HireDate { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<EmployeeProject> _projects = new();
    public IReadOnlyCollection<PositionHistory> PositionHistory => _positionHistory.AsReadOnly();
    public IReadOnlyCollection<EmployeeProject> Projects => _projects.AsReadOnly();

    private Employee() { }

    public Employee(string identificationNumber, string name, decimal salary, int currentPositionId, int departmentId, DateTime hireDate)
    {
        if (string.IsNullOrWhiteSpace(identificationNumber))
            throw new ArgumentException("Employee identification number cannot be empty.", nameof(identificationNumber));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Employee name cannot be empty.", nameof(name));

        if (salary < DomainValidationConstants.MinimumSalary)
            throw new ArgumentException("Salary cannot be negative.", nameof(salary));

        if (currentPositionId < DomainValidationConstants.MinimumEntityId)
            throw new ArgumentException($"Position ID must be at least {DomainValidationConstants.MinimumEntityId}.", nameof(currentPositionId));

        if (departmentId < DomainValidationConstants.MinimumEntityId)
            throw new ArgumentException($"Department ID must be at least {DomainValidationConstants.MinimumEntityId}.", nameof(departmentId));

        IdentificationNumber = identificationNumber.Trim();
        Name = name;
        Salary = salary;
        CurrentPositionId = currentPositionId;
        DepartmentId = departmentId;
        HireDate = hireDate;
        IsActive = true;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Employee name cannot be empty.", nameof(newName));

        Name = newName;
    }

    public void UpdateIdentificationNumber(string newIdentificationNumber)
    {
        if (string.IsNullOrWhiteSpace(newIdentificationNumber))
            throw new ArgumentException("Employee identification number cannot be empty.", nameof(newIdentificationNumber));

        IdentificationNumber = newIdentificationNumber.Trim();
    }

    public void UpdateSalary(decimal newSalary)
    {
        if (newSalary < DomainValidationConstants.MinimumSalary)
            throw new ArgumentException("Salary cannot be negative.", nameof(newSalary));

        Salary = newSalary;
    }

    public void ChangePosition(int newPositionId)
    {
        if (newPositionId < DomainValidationConstants.MinimumEntityId)
            throw new ArgumentException($"Position ID must be at least {DomainValidationConstants.MinimumEntityId}.", nameof(newPositionId));

        if (CurrentPositionId != newPositionId)
        {
            _positionHistory.Add(new PositionHistory(Id, CurrentPositionId, HireDate));
            CurrentPositionId = newPositionId;
        }
    }

    public void ChangeDepartment(int newDepartmentId)
    {
        if (newDepartmentId < DomainValidationConstants.MinimumEntityId)
            throw new ArgumentException($"Department ID must be at least {DomainValidationConstants.MinimumEntityId}.", nameof(newDepartmentId));

        DepartmentId = newDepartmentId;
    }

    public void AssignProject(EmployeeProject employeeProject)
    {
        if (_projects.Any(p => p.ProjectId == employeeProject.ProjectId))
            return;

        _projects.Add(employeeProject);
    }

    public void UnassignProject(int projectId)
    {
        EmployeeProject? project = _projects.FirstOrDefault(p => p.ProjectId == projectId);
        if (project != null)
            _projects.Remove(project);
    }

    public void UpdateProjectAssignments(IEnumerable<int> projectIds, DateTime assignmentDate)
    {
        List<int> requestedProjectIds = projectIds.Distinct().ToList();

        if (requestedProjectIds.Any(projectId => projectId < DomainValidationConstants.MinimumEntityId))
            throw new ArgumentException($"Project IDs must be at least {DomainValidationConstants.MinimumEntityId}.", nameof(projectIds));

        foreach (EmployeeProject project in _projects.Where(project => project.UnassignedDate == null && !requestedProjectIds.Contains(project.ProjectId)))
        {
            project.SetUnassignedDate(assignmentDate);
        }

        foreach (int projectId in requestedProjectIds)
        {
            EmployeeProject? existingProject = _projects.FirstOrDefault(project => project.ProjectId == projectId);

            if (existingProject == null)
            {
                _projects.Add(new EmployeeProject(Id, projectId, assignmentDate));
                continue;
            }

            existingProject.MarkAsAssigned();
        }
    }

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;
}
