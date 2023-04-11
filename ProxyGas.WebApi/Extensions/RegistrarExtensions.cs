using ProxyGas.WebApi.Registrars;

namespace ProxyGas.WebApi.Extensions;

public static class RegistrarExtensions
{
   public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
   {
       //Using reflection to get all the classes that implement IWebApplicationBuilderRegistrar
       var registrarTypes = scanningType.Assembly.GetTypes()
           .Where(t => t.GetInterfaces().Contains(typeof(IWebApplicationBuilderRegistrar)))
           .ToList();
       
         foreach (var registrarType in registrarTypes)
         {
             var registrar = (IWebApplicationBuilderRegistrar) Activator.CreateInstance(registrarType);
             registrar.Register(builder);
         }
         
         
   }

   
   public static void RegisterWebApp(this WebApplication app, Type scanningType)
   {
         //Using reflection to get all the classes that implement IWebApplicationRegistrar
            var registrarTypes = scanningType.Assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IWebApplicationRegistrar)))
                .ToList();
            
            foreach (var registrarType in registrarTypes) 
            {
                var registrar = (IWebApplicationRegistrar) Activator.CreateInstance(registrarType);
                registrar.Register(app);
            }
   }
}