using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Identity.Api.Ping.Controllers;

[ApiController]
[Route("api/ping")]
[Authorize]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "Pong", user = User.Identity?.Name });
    }
}