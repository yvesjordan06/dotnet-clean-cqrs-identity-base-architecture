using ProxyGas.DbContext;
using ProxyGas.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Add Our Registrars
builder.RegisterServices(typeof(Program));

var app = builder.Build(); //<- This builds the application giving us the possibility to use injected services

//Add our WebApp Registrars
app.RegisterWebApp(typeof(Program));

app.Run();