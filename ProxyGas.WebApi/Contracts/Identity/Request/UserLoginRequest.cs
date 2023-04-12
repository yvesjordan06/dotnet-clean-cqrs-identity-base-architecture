using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProxyGas.WebApi.Contracts.Identity.Request;

public class UserLoginRequest
{
    [EmailAddress] [Required] 
    public string Username { get; set; }

    [Required] 
    public string Password { get; set; }
}