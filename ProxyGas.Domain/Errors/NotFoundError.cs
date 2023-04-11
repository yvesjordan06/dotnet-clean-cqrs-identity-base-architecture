namespace ProxyGas.Domain.Errors;

public record NotFoundError(string Message = "The requested resource was not found") :DomainError(404, Message, "Not Found Error");