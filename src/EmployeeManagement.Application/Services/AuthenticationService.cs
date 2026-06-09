namespace EmployeeManagement.Application.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly IMediator _mediator;

    public AuthenticationService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        RegisterCommand command = new RegisterCommand
        {
            Email = request.Email,
            Password = request.Password
        };

        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        LoginCommand command = new LoginCommand
        {
            Email = request.Email,
            Password = request.Password
        };

        return await _mediator.Send(command, cancellationToken);
    }
}
