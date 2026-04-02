using System.Net;

public class RegisterResponse
{
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }
    public MeResponse Me { get; set; }
}