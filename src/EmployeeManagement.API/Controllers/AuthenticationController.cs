namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route(ApiConstants.Routes.Auth)]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost(ApiConstants.Routes.Register)]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        AuthResponse response = await _authenticationService.RegisterAsync(request, cancellationToken);
        return Created(string.Empty, response);
    }

    [HttpPost(ApiConstants.Routes.Login)]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        AuthResponse response = await _authenticationService.LoginAsync(request, cancellationToken);
        return Ok(response);
    }
}
