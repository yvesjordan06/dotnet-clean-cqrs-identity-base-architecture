using Microsoft.EntityFrameworkCore;
using ProxyGas.Application.UserProfiles.Queries;
using ProxyGas.Domain.Aggregates.UserProfiles;

namespace ProxyGas.Application.UserProfiles.QueryHandlers;

internal class GetAllUserProfilesQueryHandler : IRequestHandler<GetAllUserProfilesQuery, OneOf<IEnumerable<UserProfile>, DomainError>>
{
    private readonly ProxyGasDbContext _db;
    
    public GetAllUserProfilesQueryHandler(ProxyGasDbContext db)
    {
        _db = db;
    }
    
    
    public async Task< OneOf<IEnumerable<UserProfile>, DomainError>> Handle(GetAllUserProfilesQuery request, CancellationToken cancellationToken)
    {
      return  await _db.UserProfiles.ToListAsync(cancellationToken);
    }
}