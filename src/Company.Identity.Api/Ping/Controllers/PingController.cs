using Microsoft.AspNetCore.Mvc;

namespace Company.Identity.Api.Ping.Controllers;

[ApiController]
[Route("api/ping")]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Pong");
    }
}