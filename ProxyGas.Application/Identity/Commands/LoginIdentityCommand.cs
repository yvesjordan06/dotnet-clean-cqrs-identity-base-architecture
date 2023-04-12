namespace ProxyGas.Application.Identity.Commands;

public class LoginIdentityCommand : IRequest<OneOf<string, DomainError>>
{
    public string Username { get; set; }

    public string Password { get; set; }
}