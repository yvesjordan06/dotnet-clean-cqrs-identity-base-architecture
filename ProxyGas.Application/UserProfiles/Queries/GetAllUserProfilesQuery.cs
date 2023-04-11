using MediatR;
using OneOf;
using ProxyGas.Domain.Aggregates.UserProfiles;
using ProxyGas.Domain.Errors;

namespace ProxyGas.Application.UserProfiles.Queries;

public class GetAllUserProfilesQuery : IRequest<OneOf<IEnumerable<UserProfile>, DomainError>>
{
    
}