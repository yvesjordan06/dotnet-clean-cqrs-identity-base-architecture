using Microsoft.AspNetCore.Mvc;
using ProxyGas.Application.IdentityRoles.Query;

namespace ProxyGas.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.IdentityRoles.Base)]
[ApiController]
public class IdentityRolesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public IdentityRolesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllIdentityRoles()
    {
        //We create the query
        var query = new GetAllIdentityRolesQuery();
        
        //We send the query to the mediator
        var result = await _mediator.Send(query);
        
        //We map the result to the response
        return result.Match(
            success =>Ok(success),
            ErrorMappings.Map
        );
    }
    
    

}