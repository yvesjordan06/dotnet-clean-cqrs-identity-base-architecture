using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ProxyGas.WebApi.Registrars;

public class MvcWebAppRegistrar : IWebApplicationRegistrar
{
    public void Register(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions) // <- We iterate over all the API versions to add them to the Swagger UI (This is required for versioning)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }
        
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

    }
}