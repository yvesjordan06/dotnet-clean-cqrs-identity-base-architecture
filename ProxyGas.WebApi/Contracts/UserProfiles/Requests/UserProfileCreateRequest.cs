﻿namespace ProxyGas.WebApi.Contracts.UserProfiles.Requests;

public record UserProfileCreateRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string EmailAddress { get; init; }
    public string Phone { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string CurrentCity { get; init; }
};