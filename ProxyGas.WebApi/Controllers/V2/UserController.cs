using Microsoft.AspNetCore.Mvc;
using ProxyGas.Domain.Entities;

namespace ProxyGas.WebApi.Controllers.V2;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public ActionResult<User> GetById(Guid id)
    {
        User user = new ("yvesjordan06@gmail.com", "Hiroshima", "Yves Version 2");
        return Ok(user);
    }
    
}