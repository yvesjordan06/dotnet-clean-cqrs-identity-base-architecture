using Microsoft.AspNetCore.Identity;

namespace ProxyGas.Application.IdentityRoles.Query;

public class GetAllIdentityRolesQuery : IRequest<OneOf<IEnumerable<IdentityRole>, DomainError>>
{
    
}