using MediatR;
using OneOf;
using ProxyGas.Domain.Aggregates.UserProfiles;
using ProxyGas.Domain.Errors;

namespace ProxyGas.Application.UserProfiles.Commands;

public class UpdateUserProfileBasicInfoCommand : IRequest<OneOf<UserProfile, DomainError>>
{
    public Guid UserProfileId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? CurrentCity { get; set; }
}