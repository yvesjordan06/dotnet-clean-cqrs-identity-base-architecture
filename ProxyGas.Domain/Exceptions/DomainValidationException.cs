namespace ProxyGas.Domain.Exceptions;

public class DomainValidationException: DomainException
{
    internal DomainValidationException(string code = "Domain Entity Validation", string message =
         "One or more validation errors occurred."
        ) : base(code, message)
    {
    }
}