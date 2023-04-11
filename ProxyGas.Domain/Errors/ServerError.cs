namespace ProxyGas.Domain.Errors;

public record ServerError(string Message = "The server couldn't handle this request") :DomainError(500, Message, "Server Error");