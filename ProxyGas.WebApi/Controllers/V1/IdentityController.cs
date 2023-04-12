
using Microsoft.AspNetCore.Mvc;
using ProxyGas.Application.Identity.Commands;
using ProxyGas.WebApi.Contracts.Identity.Request;
using ProxyGas.WebApi.Contracts.Identity.Response;

namespace ProxyGas.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.Identity.Base)]
[ApiController]
public class IdentityController : ControllerBase
{
     private readonly IMediator _mediator;
     private readonly IMapper _mapper;
     
     public IdentityController(IMediator mediator, IMapper mapper)
     {
         _mediator = mediator;
         _mapper = mapper;
     }
     
      
        [HttpPost(ApiRoutes.Identity.Register)]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var command = _mapper.Map<RegisterIdentityCommand>(request);
            var result = await _mediator.Send(command);
            
            return result.Match(
                success => Ok(new AuthenticationResponse{Token =  success}),
                failure => ErrorMappings.Map(failure)
            );
        }
        
        [HttpPost(ApiRoutes.Identity.Login)]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var command = _mapper.Map<LoginIdentityCommand>(request);
            var result = await _mediator.Send(command);
            
            return result.Match(
                success => Ok(new AuthenticationResponse{Token =  success}),
                failure => ErrorMappings.Map(failure)
            );
        }
}