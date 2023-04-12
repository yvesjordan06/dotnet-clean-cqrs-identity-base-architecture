namespace ProxyGas.WebApi.Contracts.Identity.Request;

public class UserRegistrationRequest
{
    [EmailAddress] [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }

    [Required] public string ConfirmPassword { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required] public string Phone { get; set; }

    [Required] public DateTime DateOfBirth { get; set; }

    [Required] public string CurrentCity { get; set; }
}