using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

public record AuthCommand(
    string Email,
    string Password) : IRequest<AuthResponse>;

public class AuthHandler : IRequestHandler<AuthCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public AuthHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(AuthCommand request, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password");

        MeResponse me = new MeResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role
        };


        var token = _authService.GenerateToken(user);

        var response = new AuthResponse
        {
            Token = token.Token,
            ExpiresOn = token.ExpiresOn,
            Me = me
        };

        return response;
    }
}