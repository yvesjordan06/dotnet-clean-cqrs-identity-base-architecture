using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProxyGas.WebApi.Options;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    
    private readonly IApiVersionDescriptionProvider _provider;  // <- This provider gives us information about the current API versions
    
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions) // <- We iterate over all the API versions
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description)); // <- We create a Swagger document for each API version
        }

        var scheme = GetJwtSecurityScheme();
        options.AddSecurityDefinition(scheme.Reference.Id, scheme);
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { scheme, Array.Empty<string>() }
        });
    }


    /// <summary>
    ///  This method creates the Swagger document for each API version and adds the version information to the document
    /// </summary>
    /// <param name="description">
    ///  This parameter contains the information about the current API version
    /// </param>
    /// <returns>
    ///  This method returns the Swagger document for each API version
    /// </returns>
    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "ProxyGas API",
            Version = description.ApiVersion.ToString(),
            Description = "ProxyGas API",
            Contact = new OpenApiContact() { Name = "Yves Jordan", Email = "yvesjordan06@gmail.com" },
            License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        };
        
        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }
        
        return info;
    }

    private OpenApiSecurityScheme GetJwtSecurityScheme()
    {
        return new OpenApiSecurityScheme()
        {
            Name = "JWT Authentication",
            Description =  "Enter JWT Bearer token **_only_**",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme =   "bearer",
            BearerFormat = "JWT",
            Reference = new ()
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
    }


}