namespace ProxyGas.Domain.Errors;

public abstract record DomainError
{
     public int Code { get; init; }
     public string Type { get; init; }
     public string Message { get; init; }

     public DomainError(int code, string message, string type= "DomainError")
     {
           Code = code;
           Message = message;
           Type = type;
     }
}