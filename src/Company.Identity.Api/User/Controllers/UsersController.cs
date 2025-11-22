using Company.Identity.Api.User.Requests;
using Company.Identity.Api.User.Responses;
using Company.Identity.Application.User.Interfaces.Handlers;
using Company.Identity.Shared.Metadata.Attributes;
using Company.Identity.Shared.Metadata.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Company.Identity.Api.User.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(ICreateUserHandler createUserHandler) : ControllerBase
{
    [HttpPost("create")]
    [EndpointSummary("Create a new user account.")]
    [EndpointDescription("Creates a user account with the provided username, email, and password.")]
    [TableContext(TableNames.Users)]
    [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var command = request.ToCommand();
        var result = await createUserHandler.HandleAsync(command);

        if (!result.IsSuccess)
            return BadRequest(result);

        var response = CreateUserResponse.From(result.Value);
        return Ok(response);
    }
}