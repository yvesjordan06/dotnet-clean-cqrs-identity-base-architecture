using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProxyGas.Application.UserProfiles.Commands;
using ProxyGas.Application.UserProfiles.Queries;
using ProxyGas.WebApi.Contracts.UserProfiles.Requests;
using ProxyGas.WebApi.Contracts.UserProfiles.Responses;


namespace ProxyGas.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.UserProfiles.Base)]
[ApiController]
public class UserProfilesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserProfilesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUserProfiles()
    {
        //We create the query
        var query = new GetAllUserProfilesQuery();
        
        //We send the query to the mediator
        var result = await _mediator.Send(query);
        
        //We map the result to the response
        return result.Match(
            success =>Ok( _mapper.Map<IEnumerable<UserProfileResponse>>(success)),
            ErrorMappings.Map
        );

        
        
        
    }


    [HttpGet(ApiRoutes.UserProfiles.ById)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ValidateGuid("id")]
    public async Task<IActionResult> GetUserProfileById(Guid id)
    {
        //We create the query
        var query = new GetUserProfileByIdQuery{UserProfileId = id};
        
        //We send the query to the mediator
        var result = await _mediator.Send(query);
        
        
        //We map the result to the response
        return result.Match(
            success =>Ok( _mapper.Map<UserProfileResponse>(success)),
            ErrorMappings.Map
        );
        
      
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ValidateModel]
    public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileCreateRequest profile)
    {
        //First we create the command
        var command = _mapper.Map<CreateUserProfileCommand>(profile);

        //Then we send the command to the mediator
        var result = await _mediator.Send(command);

        //We map the result to the response and return it
        return result.Match(
            success => CreatedAtAction(nameof(GetUserProfileById), new {id = success.Id}, _mapper.Map<UserProfileResponse>(success)),
            ErrorMappings.Map
        );
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPatch(ApiRoutes.UserProfiles.BasicInfoById)]
    [ValidateModel]
    [ValidateGuid("id")]
    public async Task<IActionResult> UpdateUserProfileBasicInfo(Guid id, [FromBody] UserProfileBasicInfoUpdateRequest profileBasicInfo)
    {
         //Fist we create the command
        var command = _mapper.Map<UpdateUserProfileBasicInfoCommand>(profileBasicInfo);
        command.UserProfileId = id;
        
        //Then we send the command to the mediator
        var result = await _mediator.Send(command);
        
        //We map the result to the response and return it
        return result.Match(
            success => Ok(_mapper.Map<UserProfileResponse>(success)),
            ErrorMappings.Map
        );
    }
    
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //
    [HttpDelete(ApiRoutes.UserProfiles.ById)]
    public async Task<IActionResult> DeleteUserProfile(Guid id)
    {
        throw new NotImplementedException("This method is not implemented yet");
        //First we create the command
        var command = new DeleteUserProfileCommand{UserProfileId = id};
        
        //Then we send the command to the mediator
        var result = await _mediator.Send(command);
        
        //Finally we return the response
        return result.Match(
            success => NoContent(),
            ErrorMappings.Map
        );
    }
    
    
}