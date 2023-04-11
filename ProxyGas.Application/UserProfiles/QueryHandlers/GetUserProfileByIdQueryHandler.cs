using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using ProxyGas.Application.UserProfiles.Queries;
using ProxyGas.DbContext;
using ProxyGas.Domain.Aggregates.UserProfiles;
using ProxyGas.Domain.Errors;

namespace ProxyGas.Application.UserProfiles.QueryHandlers;

internal class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery,  OneOf<UserProfile, DomainError>>
{
    private readonly ProxyGasDbContext _db;
    
    public GetUserProfileByIdQueryHandler(ProxyGasDbContext db)
    {
        _db = db;
    }
    public async Task< OneOf<UserProfile, DomainError>> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
      var userProfile =  await _db.UserProfiles.FirstOrDefaultAsync(x => x.Id == request.UserProfileId, cancellationToken);
        if (userProfile == null)
        {
            return new NotFoundError("User profile not found");
        }
        
        return userProfile;
    }
}