using FluentValidation;
using MediatR;
using OneOf;
using ProxyGas.Domain.Errors;

namespace ProxyGas.Application.UserProfiles.Commands;

public class DeleteUserProfileCommand: IRequest<OneOf<Unit, DomainError>>
{
    public Guid UserProfileId { get; set; }
    
}


public class DeleteUserProfileCommandValidator : AbstractValidator<DeleteUserProfileCommand>
{
    public DeleteUserProfileCommandValidator()
    {
        RuleFor(x => x.UserProfileId).NotEmpty();
    }
}