using ProxyGas.Application;
using ProxyGas.Application.UserProfiles.Queries;

namespace ProxyGas.WebApi.Registrars;

public class BogardRegistrar : IWebApplicationBuilderRegistrar
{
    public void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(GetAllUserProfilesQuery).Assembly);
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ProxyGasApplicationReference>());
    }
}