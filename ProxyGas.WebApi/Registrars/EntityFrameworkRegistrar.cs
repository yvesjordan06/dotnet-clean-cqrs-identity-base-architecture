using Microsoft.EntityFrameworkCore;
using ProxyGas.DbContext;

namespace ProxyGas.WebApi.Registrars;

public class EntityFrameworkRegistrar : IWebApplicationBuilderRegistrar
{
    public void Register(WebApplicationBuilder builder)
    {
        var connectionString = 
            builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ProxyGasDbContext>(options =>
           options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    }
}