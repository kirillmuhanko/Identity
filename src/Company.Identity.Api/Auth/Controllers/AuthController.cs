using Company.Identity.Api.Auth.Requests;
using Company.Identity.Api.Auth.Responses;
using Company.Identity.Application.Auth.Interfaces.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Company.Identity.Api.Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ICreateUserHandler createUserHandler) : ControllerBase
{
    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var command = request.ToCommand();
        var result = await createUserHandler.HandleAsync(command);

        if (!result.IsSuccess)
            return BadRequest(result);

        var response = CreateUserResponse.FromDto(result.Value);
        return Ok(response);
    }
}