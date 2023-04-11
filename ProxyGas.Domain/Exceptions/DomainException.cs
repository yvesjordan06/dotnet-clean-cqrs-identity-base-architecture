namespace ProxyGas.Domain.Exceptions;

public abstract class DomainException : Exception
{

    public string Code { get; }
    public string Details { get; init; } = "No details provided.";
    
    public ICollection<String> Errors { get; init; } = new List<string>();

    internal DomainException(string code, string message) : base(message)
    { 
         this.Code = code;
    }
}