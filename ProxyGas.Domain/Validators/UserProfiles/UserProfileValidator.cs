using FluentValidation;
using ProxyGas.Domain.Aggregates.UserProfiles;

namespace ProxyGas.Domain.Validators.UserProfiles;

public class UserProfileValidator : AbstractValidator<UserProfile>
{
    public UserProfileValidator()
    {
        RuleFor(x => x.BasicInfo).SetValidator(new BasicInfoValidator());
        
        //The UpdatedAt Should never be older than the CreatedAt
        RuleFor(x => x.UpdatedAt).GreaterThanOrEqualTo(x => x.CreatedAt).WithMessage(
            "The UpdatedAt Should never be older than the CreatedAt"
            );
        
    }
    
}