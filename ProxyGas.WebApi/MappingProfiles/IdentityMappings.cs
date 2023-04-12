using ProxyGas.Application.Identity.Commands;
using ProxyGas.WebApi.Contracts.Identity.Request;

namespace ProxyGas.WebApi.MappingProfiles;

public class IdentityMappings : Profile
{
    public IdentityMappings()
    {
        CreateMap<UserRegistrationRequest, RegisterIdentityCommand>();
        CreateMap<UserLoginRequest, LoginIdentityCommand>();
    }
}