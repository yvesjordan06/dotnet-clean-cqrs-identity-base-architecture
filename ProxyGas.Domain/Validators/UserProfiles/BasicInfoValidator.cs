using FluentValidation;
using ProxyGas.Domain.Aggregates.UserProfiles;

namespace ProxyGas.Domain.Validators.UserProfiles;

public class BasicInfoValidator :  AbstractValidator<BasicInfo>
{
    public BasicInfoValidator()
    {
        RuleFor(x=>x.FirstName).MinimumLength(3).WithMessage("First name must be at least 3 characters long");
        RuleFor(x => x.LastName).MinimumLength(3).WithMessage("Last name must be at least 3 characters long");
        RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Email address is not valid");
        //Regex for phone number: must start with + then be follow be minimum 10 digits
        RuleFor(x => x.Phone).Matches(@"^\+\d{10,}$").WithMessage("Phone number is not valid, must start with + and be at least 10 digits long eg: +237691234567");
        //Minimum year is 1900 and maximum date is today
        RuleFor(x => x.DateOfBirth).InclusiveBetween(new DateTime(1900, 1, 1), DateTime.Now);
        RuleFor(x => x.CurrentCity).NotEmpty().WithMessage("Current city is required");
    }
}