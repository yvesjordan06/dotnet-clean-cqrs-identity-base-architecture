using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProxyGas.Application.Identity.Values;
using ProxyGas.Application.Options;

namespace ProxyGas.Application.Services;

public class IdentityService
{
    private readonly JwtSettings _jwtSettings;
    private readonly byte[] _key;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityService(IOptions<JwtSettings> options)
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
    
    public ICollection<Claim> GetIdentityClaims(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypesValues.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypesValues.Email, user.Email!),
            new Claim(ClaimTypesValues.Sub, user.Id),
            new Claim(ClaimTypesValues.IdentityId, user.Id),
        };
        
        //Get the user's claims
        var userClaims = _userManager.GetClaimsAsync(user).Result;
        
        //Add the user's claims
        claims.AddRange(userClaims);
        
        //Get the user's roles
        var userRoles = _userManager.GetRolesAsync(user).Result;
        
        //For each role, get the role's claims
        foreach (var userRole in userRoles)
        {
            var role = _roleManager.FindByNameAsync(userRole).Result;
            
            if(role is null) continue;
            
            //Add the role claims
            claims.Add(GetRoleClaim(role));
            var roleClaims = _roleManager.GetClaimsAsync(role).Result;
            claims.AddRange(roleClaims);
        }
        return claims;
    }

    
    private static Claim GetRoleClaim(IdentityRole role)
    {
        return new Claim(ClaimTypesValues.Role, role.Name);
    }
}