using ProxyGas.Application.Services;

namespace ProxyGas.WebApi.Registrars;


public class ApplicationLayerRegistrar : IWebApplicationBuilderRegistrar
{
    public void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IdentityService>();
    }
}