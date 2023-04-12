using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProxyGas.Application.Identity.Values;

public static class ClaimTypesValues
{
      public const string IdentityId = "IdentityId";
      public const string UserProfileId = "UserProfileId";
      public const string Email = JwtRegisteredClaimNames.Email;
      public const string Sub = JwtRegisteredClaimNames.Sub;
      public const string Jti = JwtRegisteredClaimNames.Jti;
      public const string Role = ClaimTypes.Role;
}