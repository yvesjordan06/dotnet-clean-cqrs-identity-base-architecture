using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProxyGas.WebApi.Contracts.Errors;

namespace ProxyGas.WebApi.Filters;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(new ErrorResponse()
            {
                StatusCode = 400,
                StatusPhrase = "Bad Request",
                Errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToList(),
                Timestamp = DateTime.UtcNow,
                Message = "One or more validation errors occurred."
            });
        }
    }
}