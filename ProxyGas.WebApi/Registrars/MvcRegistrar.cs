using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using ProxyGas.WebApi.Contracts.Errors;
using ProxyGas.WebApi.Filters;

namespace ProxyGas.WebApi.Registrars;

public class MvcRegistrar : IWebApplicationBuilderRegistrar
{
    public void Register(WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionFilters>();
            options.Filters.Add<ValidateModelAttribute>();
        });


        //Add versioning
        builder.Services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);

            // This makes the default version the default version when no version is specified
            config.AssumeDefaultVersionWhenUnspecified = true;

            //  This makes the API version information available in the response headers
            config.ReportApiVersions = true;

            // config.ApiVersionReader = new HeaderApiVersionReader("api-version"); <- Use this for header versioning
            config.ApiVersionReader = new UrlSegmentApiVersionReader(); // <- Use this for url versioning
        });

        // Add the versioned API explorer, which also adds IApiVersionDescriptionProvider service
        //This is required for Swagger to work with versioning
        builder.Services.AddVersionedApiExplorer(config =>
        {
            //Version format: 'v'major[.minor][-status]
            config.GroupNameFormat = "'v'VVV";
            
            //Replace the version in the URL with the declared version of the action
            config.SubstituteApiVersionInUrl = true; 
        });
        
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
    }
}