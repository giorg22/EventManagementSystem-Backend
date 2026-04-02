using System.Net;

public class AuthResponse
{
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }
    public MeResponse Me { get; set; }
}