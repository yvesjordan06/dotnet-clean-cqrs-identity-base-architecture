using AutoMapper;
using MediatR;
using OneOf;
using ProxyGas.Application.UserProfiles.Commands;
using ProxyGas.DbContext;
using ProxyGas.Domain.Aggregates.UserProfiles;
using ProxyGas.Domain.Errors;

namespace ProxyGas.Application.UserProfiles.CommandHandlers;

public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, OneOf<UserProfile, DomainError>>
{
    private readonly ProxyGasDbContext _db;

    public CreateUserProfileCommandHandler(ProxyGasDbContext db)
    {
        _db = db;
    }


    public async Task< OneOf<UserProfile, DomainError>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var basicInfo = BasicInfo.Create(
            firstName: request.FirstName,
            lastName: request.LastName,
            emailAddress: request.EmailAddress,
            phone: request.Phone,
            dateOfBirth: request.DateOfBirth,
            currentCity: request.CurrentCity
        );

        var userProfile = UserProfile.Create(Guid.NewGuid().ToString(), basicInfo);
        
        _db.UserProfiles.Add(userProfile);
        await _db.SaveChangesAsync(cancellationToken);

        return userProfile;
    }
}