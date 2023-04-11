using MediatR;
using OneOf;
using ProxyGas.Domain.Aggregates.UserProfiles;
using ProxyGas.Domain.Errors;

namespace ProxyGas.Application.UserProfiles.Commands;

public class CreateUserProfileCommand : IRequest<OneOf<UserProfile, DomainError>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CurrentCity { get; set; }
}

