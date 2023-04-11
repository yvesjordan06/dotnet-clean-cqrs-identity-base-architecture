using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProxyGas.Domain.Errors;
using ProxyGas.WebApi.Contracts.Errors;

namespace ProxyGas.WebApi.MappingProfiles;
using AutoMapper;

public class ErrorMappings
{
    public static IActionResult Map(DomainError error)
    {
        
      //if error is NotFoundError return NotFound
        //if error is ValidationError return BadRequest
        //if error is UnauthorizedError return Unauthorized
        
        //Q: If my error inherits from DomainError, how can I map it to the correct HttpResult?
        //A: Use the switch expression
        return error switch
        {
            NotFoundError notFoundError => new NotFoundObjectResult(new ErrorResponse()
            {
                StatusCode = 404,
                StatusPhrase = "Not Found",
                Errors = new List<string>() {error.Message},
                Timestamp = DateTime.UtcNow,
                Message = error.Message,
            }),
            ValidationError validationError => new BadRequestObjectResult(new ErrorResponse()
            {
                StatusCode = 400,
                StatusPhrase = "Bad Request",
                Errors = validationError.Errors.ToList(),
                Message = validationError.Message,
            }),
            ServerError serverError  =>  new ObjectResult(new ErrorResponse()
            {
                StatusCode = 500,
                StatusPhrase = "Server Error",
                Errors =new List<string>()
                {
                    error.Message,
                },
                Message = error.Message,

            })
            {
                StatusCode = 500
            }
        };
    }
}