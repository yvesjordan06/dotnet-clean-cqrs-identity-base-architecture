using MediatR;
using OneOf;
using ProxyGas.Domain.Aggregates.UserProfiles;
using ProxyGas.Domain.Errors;

namespace ProxyGas.Application.UserProfiles.Queries;

public class GetUserProfileByIdQuery : IRequest<OneOf<UserProfile, DomainError>>
{
      public Guid UserProfileId { get; set; }   
      
      
}