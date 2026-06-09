namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route(ApiConstants.Routes.Employees)]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    [Authorize(Roles = ApiConstants.AdminOrUserRoles)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        IReadOnlyCollection<EmployeeResponse> employees = await _employeeService.GetAllEmployeesAsync(cancellationToken);
        return Ok(employees);
    }

    [HttpGet(ApiConstants.Routes.Id)]
    [Authorize(Roles = ApiConstants.AdminOrUserRoles)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        EmployeeDetailsResponse employee = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
        return Ok(employee);
    }

    [HttpGet(ApiConstants.Routes.DepartmentWithProjects)]
    [Authorize(Roles = ApiConstants.AdminOrUserRoles)]
    public async Task<IActionResult> GetByDepartmentWithProjects(int departmentId, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<EmployeeResponse> employees = await _employeeService.GetEmployeesByDepartmentWithProjectsAsync(departmentId, cancellationToken);
        return Ok(employees);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        int employeeId = await _employeeService.CreateEmployeeAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = employeeId }, new { Id = employeeId });
    }

    [HttpPut(ApiConstants.Routes.Id)]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeRequest request, CancellationToken cancellationToken)
    {
        await _employeeService.UpdateEmployeeAsync(id, request, cancellationToken);
        return NoContent();
    }

    [HttpDelete(ApiConstants.Routes.Id)]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _employeeService.DeleteEmployeeAsync(id, cancellationToken);
        return NoContent();
    }
}
