using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProxyGas.WebApi.Contracts.Errors;

namespace ProxyGas.WebApi.Filters;

public class ValidateGuidAttribute : ActionFilterAttribute
{
    private readonly string _key;
    private bool hasError = false;

    public ValidateGuidAttribute(string key)
    {
        _key = key;
    }


    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //If there is no value associated to the key, we Stop
        if (!context.ActionArguments.TryGetValue(_key, out var value)) return;

        //If we could parse the Guid, we stop
        if (Guid.TryParse(value?.ToString(), out var guid)) return;

        //Set the result to error
        {
            hasError = true;
            Console.WriteLine("It is false");
            context.Result = new BadRequestObjectResult(new ErrorResponse()
            {
                StatusCode = 400,
                StatusPhrase = "Bad Request",
                Errors = new List<string>()
                {
                    $"The identifier for key {_key} is not a valid Guid format, It should be in the format {Guid.Empty}"
                },
                Timestamp = DateTime.UtcNow,
                Message = "One or more validation errors occurred."
            });
        }
    }

    //On Result Executing
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine("Executing On Result");
        if (hasError)
        {
            context.Result = new BadRequestObjectResult(new ErrorResponse()
            {
                StatusCode = 400,
                StatusPhrase = "Bad Request",
                Errors = new List<string>()
                {
                    $"The identifier for key {_key} is not a valid Guid format, It should be in the format {Guid.Empty}"
                },
                Timestamp = DateTime.UtcNow,
                Message = "One or more validation errors occurred."
            });
        }
    }
}