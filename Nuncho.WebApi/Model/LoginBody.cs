namespace Nuncho.WebApi.Model;

public class LoginBody
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}