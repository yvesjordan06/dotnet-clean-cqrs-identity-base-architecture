using AutoMapper;
using ProxyGas.Application.UserProfiles.Commands;
using ProxyGas.Domain.Aggregates.UserProfiles;
using ProxyGas.WebApi.Contracts.UserProfiles.Requests;
using ProxyGas.WebApi.Contracts.UserProfiles.Responses;

namespace ProxyGas.WebApi.MappingProfiles;

public class UserProfileMappings : Profile
{
    public UserProfileMappings()
    {
        // Map from the request to the command
        CreateMap<UserProfileCreateRequest, CreateUserProfileCommand>();
        CreateMap<UserProfileBasicInfoUpdateRequest, UpdateUserProfileBasicInfoCommand>();
        
        
        // Map domain to response
        CreateMap<UserProfile, UserProfileResponse>();
        CreateMap<BasicInfo, BasicInfoResponse>();
    }
}