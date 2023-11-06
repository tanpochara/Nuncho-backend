namespace Nuncho.WebApi.Model;

public class JwtConfig
{
    public string Secret { get; set; }
    public int ExpirationInMinutes { get; set; }
}