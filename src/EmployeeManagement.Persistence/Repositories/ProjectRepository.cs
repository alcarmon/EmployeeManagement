namespace EmployeeManagement.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly EmployeeManagementDbContext _context;

    public ProjectRepository(EmployeeManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(int projectId)
    {
        return await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(project => project.Id == projectId);
    }

    public async Task<Project?> GetByNameAsync(string name)
    {
        return await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(project => project.Name == name);
    }

    public async Task<IReadOnlyCollection<Project>> GetAllAsync()
    {
        return await _context.Projects
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(int projectId)
    {
        return await _context.Projects
            .AnyAsync(project => project.Id == projectId);
    }

    public async Task AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
    }

    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int projectId)
    {
        Project? project = await _context.Projects.FindAsync(projectId);
        if (project != null)
        {
            _context.Projects.Remove(project);
        }
    }
}
