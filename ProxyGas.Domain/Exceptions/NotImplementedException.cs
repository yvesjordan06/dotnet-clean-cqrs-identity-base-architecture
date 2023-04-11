namespace ProxyGas.Domain.Exceptions;

public class NotImplementedException : DomainException
{
    internal NotImplementedException(string code = "not_implemented", string message = "Not implemented" ) : base(code, message)
    {
        this.Details = "This feature is not implemented yet";
    }
     
}