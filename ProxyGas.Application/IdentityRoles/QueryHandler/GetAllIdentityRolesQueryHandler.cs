using Microsoft.AspNetCore.Identity;
using ProxyGas.Application.IdentityRoles.Query;

namespace ProxyGas.Application.IdentityRoles.QueryHandler;

public class GetAllIdentityRolesQueryHandler : IRequestHandler<GetAllIdentityRolesQuery, OneOf<IEnumerable<IdentityRole>, DomainError>>
{
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public GetAllIdentityRolesQueryHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    
    public async Task<OneOf<IEnumerable<IdentityRole>, DomainError>> Handle(GetAllIdentityRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
        return roles;
    }
}