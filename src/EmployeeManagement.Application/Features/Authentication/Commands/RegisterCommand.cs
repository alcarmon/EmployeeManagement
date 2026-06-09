namespace EmployeeManagement.Application.Features.Authentication.Commands;

public class RegisterCommand : IRequest<AuthResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        bool userExists = await _unitOfWork.Users.ExistsByEmailAsync(request.Email);
        if (userExists)
            throw new InvalidOperationException($"User with email {request.Email} already exists.");

        string passwordHash = _passwordHasher.HashPassword(request.Password);
        User user = new User(request.Email, passwordHash, Roles.User);

        await _unitOfWork.Users.AddAsync(user);
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
