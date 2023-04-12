namespace ProxyGas.Application.Identity.Commands;

public class RegisterIdentityCommand  :  IRequest<OneOf<string, DomainError>>
{
    public string Username { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string CurrentCity { get; set; }
}