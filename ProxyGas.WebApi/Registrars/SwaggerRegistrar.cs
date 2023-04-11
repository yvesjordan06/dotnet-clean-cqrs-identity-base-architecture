using ProxyGas.WebApi.Options;

namespace ProxyGas.WebApi.Registrars;

public class SwaggerRegistrar: IWebApplicationBuilderRegistrar
{
    public void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();

        //We add the Swagger generator we created in the Options folder
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
    }
}