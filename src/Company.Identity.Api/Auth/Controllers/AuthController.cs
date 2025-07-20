using Company.Identity.Api.Auth.Requests;
using Company.Identity.Api.Auth.Responses;
using Company.Identity.Application.Auth.Interfaces.Handlers;
using Company.Identity.Shared.Metadata.Attributes;
using Company.Identity.Shared.Metadata.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Company.Identity.Api.Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ICreateUserHandler createUserHandler) : ControllerBase
{
    [HttpPost("create-user")]
    [EndpointSummary("Create a new user account.")]
    [EndpointDescription("Creates a user account with the provided username, email, and password.")]
    [TableContext(TableNames.Users)]
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