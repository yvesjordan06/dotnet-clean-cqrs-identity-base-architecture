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
using ProxyGas.Domain.Aggregates.UserProfiles;

namespace ProxyGas.Application.Identity.CommandHandlers;

public class RegisterIdentityCommandHandler : IRequestHandler<RegisterIdentityCommand, OneOf<string, DomainError>>
{
    private readonly ProxyGasDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
   private readonly IdentityService _identityService;

    public RegisterIdentityCommandHandler(ProxyGasDbContext db, UserManager<IdentityUser> userManager, IdentityService identityService)
    {
        _db = db;
        _userManager = userManager;
        _identityService = identityService;
    }

    public async Task<OneOf<string, DomainError>> Handle(RegisterIdentityCommand request,
        CancellationToken cancellationToken)
    {
        //Ensure the user is not existing
        var user = await _userManager.FindByEmailAsync(request.Username);

        if (user != null)
        {
            return new ValidationError("User already exists");
        }

        //Create the user
        user = new IdentityUser
        {
            UserName = request.Username,
            Email = request.Username,
            EmailConfirmed = false,
            PhoneNumber =  request.Phone,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
        };

        //Begin the transaction
        using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);

        //Create the user
        var result = await _userManager.CreateAsync(user, request.Password);

        //If the user was not created, return the error
        if (!result.Succeeded)
        {
            
            //Rollback the transaction
            await transaction.RollbackAsync(cancellationToken);
            return new ValidationError("Could not create user")
            {
                Errors = result.Errors.Select(x => x.Description).ToList()
            };
            
            
        }

        //Create the user profile's basic info
        var basicInfo = BasicInfo.Create(
            firstName: request.FirstName,
            lastName: request.LastName,
            emailAddress: request.Username,
            phone: request.Phone,
            dateOfBirth: request.DateOfBirth,
            currentCity: request.CurrentCity
        );
        
        //Create the user profile
        var userProfile = UserProfile.Create(user.Id, basicInfo);
        
        //Add the user profile to the database
        await _db.UserProfiles.AddAsync(userProfile, cancellationToken);
        
        //Save the changes
        await _db.SaveChangesAsync(cancellationToken);
        
        //Commit the transaction
        await transaction.CommitAsync(cancellationToken);
        
        //Return the token
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