using Microsoft.AspNetCore.Mvc;

namespace Company.Identity.Api.Health.Controllers;

[ApiController]
[Route("api/health")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Perform a basic health check.")]
    [EndpointDescription("Returns a simple health check response indicating the service is running.")]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "Healthy",
            timestamp = DateTime.UtcNow
        });
    }
}