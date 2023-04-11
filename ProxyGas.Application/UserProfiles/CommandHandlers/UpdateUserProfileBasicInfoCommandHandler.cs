using AutoMapper;
using MediatR;
using OneOf;
using ProxyGas.Application.UserProfiles.Commands;
using ProxyGas.DbContext;
using ProxyGas.Domain.Aggregates.UserProfiles;
using ProxyGas.Domain.Errors;

namespace ProxyGas.Application.UserProfiles.CommandHandlers;

public class UpdateUserProfileBasicInfoCommandHandler : IRequestHandler<UpdateUserProfileBasicInfoCommand, OneOf<UserProfile, DomainError>>
{
     private ProxyGasDbContext _db;
     
        public UpdateUserProfileBasicInfoCommandHandler(ProxyGasDbContext db)
        {
            _db = db;
        }
    
    public async Task< OneOf<UserProfile, DomainError>> Handle(UpdateUserProfileBasicInfoCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _db.UserProfiles.FindAsync(request.UserProfileId);
        if (userProfile == null)
        {
            return new NotFoundError("User profile not found");
        }
        
        userProfile.UpdateBasicInfo(
           BasicInfo.Create(
               firstName: request.FirstName ?? userProfile.BasicInfo.FirstName,
               lastName: request.LastName ?? userProfile.BasicInfo.LastName,
               emailAddress: request.EmailAddress ?? userProfile.BasicInfo.EmailAddress,
               phone: request.Phone ?? userProfile.BasicInfo.Phone,
               dateOfBirth: request.DateOfBirth ?? userProfile.BasicInfo.DateOfBirth,
               currentCity: request.CurrentCity ?? userProfile.BasicInfo.CurrentCity
               )
        );
        
        await _db.SaveChangesAsync(cancellationToken);
        
        return userProfile;
    }
}