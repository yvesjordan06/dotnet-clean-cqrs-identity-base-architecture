using ProxyGas.Domain.Helpers;
using ProxyGas.Domain.Validators.UserProfiles;

namespace ProxyGas.Domain.Aggregates.UserProfiles;

public class UserProfile
{
    private UserProfile()
    {
        
    }
    public Guid Id { get; private set; }
    public string IdentityId { get; private set; }
    public BasicInfo BasicInfo { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    
    /// <summary>
    ///  Creates a new user profile.
    /// </summary>
    /// <param name="identityId">
    /// The identity id of the user. (i.e. the id of the user in the identity server)
    /// </param>
    /// <param name="basicInfo">
    /// The basic info of the user.
    ///  This is the info that is required to create a user profile.
    /// </param>
    /// <returns>
    /// A new user profile.
    /// </returns>
    public static UserProfile Create(string identityId, BasicInfo basicInfo)
    {
        var validator = new UserProfileValidator();
        var userProfile = new UserProfile
        {
            IdentityId = identityId,
            BasicInfo = basicInfo,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        return DomainValidator.ValidateAndThrow(userProfile, validator.Validate(userProfile));
    }
    
    
    /// <summary>
    ///  Updates the basic info of the user profile.
    /// </summary>
    /// <param name="basicInfo">
    /// The new basic info of the user.
    /// </param>
    public void UpdateBasicInfo(BasicInfo basicInfo)
    {
        //Since we are getting a BasicInfo object, and the only way to create a BasicInfo object is through the BasicInfo.Create() method,
        //we can be sure that the BasicInfo object is valid.
        BasicInfo = basicInfo; 
        UpdatedAt = DateTime.UtcNow;
    }
}