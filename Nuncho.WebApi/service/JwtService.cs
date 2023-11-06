using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Agoda.IoC.Core;
using Microsoft.IdentityModel.Tokens;
using Nuncho.WebApi.entities;
using Nuncho.WebApi.Model;
using Nuncho.WebApi.repository;

namespace Nuncho.WebApi.service;

[RegisterTransient]
public class JwtService
{
    private readonly JwtConfig _jwtConfig;
    private readonly UserRepository _userRepository;

    public JwtService(JwtConfig jwtConfig, UserRepository userRepository)
    {
        this._jwtConfig = jwtConfig;
        this._userRepository = userRepository;
    }

    public string GenerateJwt(string email, int id)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public async Task<User?> DecodeJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.Secret));

        // Configure validation parameters
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,
            ValidateIssuer = false, // Customize as needed
            ValidateAudience = false, // Customize as needed
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // Adjust as needed
        };

        try
        {
            SecurityToken validatedToken;
            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

            // Find the "Email" claim
            var emailClaim = claimsPrincipal.FindFirst(ClaimTypes.Email);

            if (emailClaim == null)
            {
                return null;
            }

            var user = await this._userRepository.GetByUsername(emailClaim.Value);
            return user;
            // Email claim not found
        }
        catch (Exception)
        {
            // Token validation failed
            return null;
        }
    }
}