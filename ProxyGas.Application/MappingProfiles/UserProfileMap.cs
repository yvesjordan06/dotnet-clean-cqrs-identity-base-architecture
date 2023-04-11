using AutoMapper;
using ProxyGas.Application.UserProfiles.Commands;
using ProxyGas.Domain.Aggregates.UserProfiles;

namespace ProxyGas.Application.MappingProfiles;

public class UserProfileMap :  Profile
{

    public UserProfileMap()
    {
        CreateMap<CreateUserProfileCommand, BasicInfo>();
    }
    
}