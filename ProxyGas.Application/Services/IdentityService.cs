using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProxyGas.Application.Options;

namespace ProxyGas.Application.Services;

public class IdentityService
{
    private readonly JwtSettings _jwtSettings;
    private readonly byte[] _key;
    
    public IdentityService( IOptions<JwtSettings> options)
    {
       
        _jwtSettings = options.Value;
        _key = Encoding.UTF8.GetBytes(_jwtSettings.SigningKey);
    }
    
    public SecurityToken CreateSecurityToken(ClaimsIdentity identity)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = GetTokenDescriptor(identity);
        return tokenHandler.CreateToken(tokenDescriptor);
    }
    
    public string WriteToken(SecurityToken token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
    
    
    private  SecurityTokenDescriptor GetTokenDescriptor(ClaimsIdentity identity)
    {
        return new()
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddDays(7),
            Audience = _jwtSettings.Audiences[0],
            Issuer = _jwtSettings.Issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
                SecurityAlgorithms.HmacSha256)
        };
    }
    
}