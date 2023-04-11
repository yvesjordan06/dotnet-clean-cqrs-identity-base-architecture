using MediatR;
using OneOf;
using ProxyGas.Application.UserProfiles.Commands;
using ProxyGas.DbContext;
using ProxyGas.Domain.Errors;

namespace ProxyGas.Application.UserProfiles.CommandHandlers;

public class DeleteUserProfileCommandHandler:  IRequestHandler<DeleteUserProfileCommand, OneOf<Unit, DomainError>>
{
    private readonly ProxyGasDbContext _context;

    public DeleteUserProfileCommandHandler(ProxyGasDbContext context)
    {
        _context = context;
    }

    public async Task<OneOf<Unit, DomainError>> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _context.UserProfiles.FindAsync(request.UserProfileId);

        if (userProfile == null)
        {
            return new NotFoundError("User profile not found");
        }

        _context.UserProfiles.Remove(userProfile);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}