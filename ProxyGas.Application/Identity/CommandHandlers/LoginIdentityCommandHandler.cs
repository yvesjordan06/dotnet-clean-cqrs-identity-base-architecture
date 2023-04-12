using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProxyGas.Application.Identity.Commands;
using ProxyGas.Application.Identity.Values;
using ProxyGas.Application.Options;
using ProxyGas.Application.Services;

namespace ProxyGas.Application.Identity.CommandHandlers;

public class LoginIdentityCommandHandler : IRequestHandler<LoginIdentityCommand, OneOf<string, DomainError>>
{
    private readonly ProxyGasDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IdentityService _identityService;
    
    public LoginIdentityCommandHandler(ProxyGasDbContext db, UserManager<IdentityUser> userManager, IdentityService identityService)
    {
        _db = db;
        _userManager = userManager;
        _identityService = identityService;
    }
  
    public async Task<OneOf<string, DomainError>> Handle(LoginIdentityCommand request, CancellationToken cancellationToken)
    {
        //Ensure the user is not existing
        var user = await _userManager.FindByEmailAsync(request.Username);
        if (user == null)
        {
            return new ValidationError("User does not exist");
        }
        //Check the password
        var result = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!result)
        {
            return new ValidationError("Invalid password");
        }
        
        //Get the user profile
        var userProfile = await _db.UserProfiles.FirstOrDefaultAsync(x => x.IdentityId == user.Id, cancellationToken);

        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypesValues.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypesValues.Email, user.Email!),
            new Claim(ClaimTypesValues.Sub, user.Id),
            new Claim(ClaimTypesValues.IdentityId, user.Id),
            new Claim(ClaimTypesValues.UserProfileId, userProfile?.Id.ToString() ?? string.Empty),
        });
        
        //Create the token
        var token = _identityService.CreateSecurityToken(claimsIdentity);
        
        //Write the token
        var tokenString = _identityService.WriteToken(token);
        
        //Return the token
        return tokenString;
    }
}