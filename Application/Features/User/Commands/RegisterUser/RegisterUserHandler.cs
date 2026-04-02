using MediatR;
using Microsoft.AspNetCore.Identity;

public record RegisterUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    UserRole role) : IRequest<RegisterResponse>;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public RegisterUserHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<RegisterResponse> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        if (await _userRepository.EmailExistsAsync(request.Email))
            throw new UnauthorizedAccessException("Email already exists");

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = PasswordHasher.HashPassword(request.Password),
            Role = request.role,
        };

        var created = await _userRepository.AddAsync(user);

        MeResponse me = new MeResponse
        {
            Id = created.Id,
            FirstName = created.FirstName,
            LastName= created.LastName,
            Email = created.Email,
            Role = user.Role
        };

        var token = _authService.GenerateToken(user);

        var response = new RegisterResponse
        {
            Token = token.Token,
            ExpiresOn = token.ExpiresOn,
            Me = me
        };

        return response;
    }
}