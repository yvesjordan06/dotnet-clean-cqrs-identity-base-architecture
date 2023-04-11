namespace ProxyGas.Domain.Errors;

public record ValidationError:DomainError
{
    
    public ICollection<string> Errors { get; init; } = new List<string>();
     public ValidationError(
         string message  = "One or more validation errors occurred."
         ):base(400, message, "Validator Error"){}
}