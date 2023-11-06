using Agoda.IoC.Core;
using Nuncho.WebApi.entities;
using Nuncho.WebApi.repository;

namespace Nuncho.WebApi.service;

[RegisterTransient]
public class AuthService
{
    private readonly UserRepository _userRepository;
    private readonly JwtService _jwtService;
    
    public AuthService(UserRepository userRepository, JwtService jwtService)
    {
        this._userRepository = userRepository;
        this._jwtService = jwtService;
    }
    
    //write a code to login and give user back jwt
    public async Task<string> SignIn(string username, string password)
    {
        var user = await _userRepository.GetByUsername(username);
        if(user == null)
        {
            throw new Exception("invalid credentials");
        }
        if (user.Password != password)
        {
            throw new Exception("invalid credentials");
        }

        var token = _jwtService.GenerateJwt(user.Email, user.Id);
        return token;
    }

    public async Task<string> SignUp(string email, string password, string name)
    {
        var user = new User()
        {
            Name = name,
            Password = password,
            Email = email
        };
        
        await _userRepository.Add(user);
        var token = _jwtService.GenerateJwt(user.Email, user.Id);
        return token;
    }
}