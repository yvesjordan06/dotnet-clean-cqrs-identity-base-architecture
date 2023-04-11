namespace ProxyGas.WebApi.Contracts.UserProfiles.Responses;

public record UserProfileResponse
{
      public Guid Id { get; init; }
      public BasicInfoResponse BasicInfo { get; init; }
      public DateTime CreatedAt { get; init; }
      public DateTime UpdatedAt { get; init; }
};