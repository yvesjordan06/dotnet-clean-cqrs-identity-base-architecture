using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProxyGas.Application.Identity.Values;

namespace ProxyGas.WebApi.Extensions;

public static class ControllerBaseUserExtensions
{
    
    /// <summary>
    ///   Gets the user profile id from the claims
    /// </summary>
    /// <param name="controllerBase"></param>
    /// <returns></returns>
    public static Guid GetUserProfileIdClaim(this ControllerBase controllerBase)
    {
        return Guid.Parse(controllerBase.User.Claims.First(c => c.Type == ClaimTypesValues.UserProfileId).Value);
    }

    /// <summary>
    ///  Gets the identity id from the claims
    /// </summary>
    /// <param name="controllerBase"></param>
    /// <returns></returns>
    public static Guid GetIdentityIdClaim(this ControllerBase controllerBase)
    {
        return Guid.Parse(controllerBase.User.Claims.First(c => c.Type == ClaimTypesValues.IdentityId).Value);
    }

    /// <summary>
    ///  Gets the user profile email from the claims
    /// </summary>
    /// <param name="controllerBase"></param>
    /// <returns></returns>
    public static string GetUserProfileEmailClaim(this ControllerBase controllerBase)
    {
        return controllerBase.User.Claims.First(c => c.Type == ClaimTypesValues.Email).Value;
    }
    
}