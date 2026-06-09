namespace EmployeeManagement.Application.Features.Authentication.Commands;

public class LoginCommand : IRequest<AuthResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
        
        if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("User account is inactive.");

        user.RecordLogin();
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        string token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.Role);

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Token = token,
            Role = user.Role
        };
    }
}
