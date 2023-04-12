using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProxyGas.Application.Options;

namespace ProxyGas.WebApi.Registrars;

public class IdentityRegistrar : IWebApplicationBuilderRegistrar
{
    public void Register(WebApplicationBuilder builder)
    {
        var jwtSettings = new JwtSettings();
        builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudiences = jwtSettings.Audiences,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey))
            };
            options.Audience = jwtSettings.Audiences[0];
            options.ClaimsIssuer = jwtSettings.Issuer;
        });
    }
}