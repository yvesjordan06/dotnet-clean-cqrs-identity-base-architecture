using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProxyGas.Domain.Errors;
using ProxyGas.Domain.Exceptions;
using ProxyGas.WebApi.Contracts.Errors;
using NotImplementedException = ProxyGas.Domain.Exceptions.NotImplementedException;

namespace ProxyGas.WebApi.MappingProfiles;
using AutoMapper;

public class DomainExceptionToErrorMapping
{
    public static DomainError Map(DomainException exception)
    {
        
      //if error is NotFoundError return NotFound
        //if error is ValidationError return BadRequest
        //if error is UnauthorizedError return Unauthorized
        
        //Q: If my error inherits from DomainError, how can I map it to the correct HttpResult?
        //A: Use the switch expression
        return exception switch
        {
            DomainValidationException validationException => new ValidationError(exception.Message)
            {
                Errors = exception.Errors.ToList(),
            },
            NotImplementedException => new ServerError(exception.Message),
            _ => new ServerError(exception.Message)
        };
    }
}