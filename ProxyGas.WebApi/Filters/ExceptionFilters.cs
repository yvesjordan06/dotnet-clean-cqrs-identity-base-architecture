using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProxyGas.Domain.Errors;
using ProxyGas.Domain.Exceptions;
using ProxyGas.WebApi.Contracts.Errors;
using ProxyGas.WebApi.MappingProfiles;

namespace ProxyGas.WebApi.Filters;

public class ExceptionFilters : ExceptionFilterAttribute
{

    public override void OnException(ExceptionContext context)
    {
      
        context.Result = context.Exception switch
        {
            DomainException domainException => ErrorMappings.Map(DomainExceptionToErrorMapping.Map(domainException)),
            _ => ErrorMappings.Map(new ServerError(context.Exception.Message))
        };
    }
}
