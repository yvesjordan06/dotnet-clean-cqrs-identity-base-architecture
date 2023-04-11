using FluentValidation;
using ProxyGas.Domain.Exceptions;
using ProxyGas.Domain.Helpers;
using ProxyGas.Domain.Validators.UserProfiles;

namespace ProxyGas.Domain.Aggregates.UserProfiles;

public class BasicInfo
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string EmailAddress { get; private set; }
    public string Phone { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string CurrentCity { get; private set; }
    
    private BasicInfo()
    {
        
    }
    
    //#Factories Methods
    /// <summary>
    ///  Creates a new instance of <see cref="BasicInfo"/>
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="emailAddress"></param>
    /// <param name="phone"></param>
    /// <param name="dateOfBirth"></param>
    /// <param name="currentCity"></param>
    /// <returns></returns>
    ///
    ///<exception cref="DomainValidationException">Thrown if the basic info is invalid</exception>
    public static  BasicInfo Create(string firstName, string lastName, string emailAddress, string phone, DateTime dateOfBirth, string currentCity)
    {
       
        var validator = new BasicInfoValidator();

        var basicInfo = new BasicInfo
        {
            FirstName = firstName,
            LastName = lastName,
            EmailAddress = emailAddress,
            Phone = phone,
            DateOfBirth = dateOfBirth,
            CurrentCity = currentCity
        };

        var validationResult = validator.Validate(basicInfo);

        return DomainValidator.ValidateAndThrow(basicInfo, validationResult);
    }
    
    
}