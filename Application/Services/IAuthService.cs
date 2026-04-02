using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
public interface IAuthService
{
    public AuthResponse GenerateToken(User user);
}
