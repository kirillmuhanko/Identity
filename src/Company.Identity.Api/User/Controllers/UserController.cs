using Company.Identity.Api.User.Requests;
using Company.Identity.Api.User.Responses;
using Company.Identity.Application.User.Commands;
using Company.Identity.Application.User.Interfaces.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Company.Identity.Api.User.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(ICreateUserHandler createUserHandler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var command = new CreateUserCommand
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await createUserHandler.HandleAsync(command);

        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }

        var response = new CreateUserResponse
        {
            Id = Guid.NewGuid(), // This should ideally come from the created user entity
            UserName = request.UserName,
            Email = request.Email
        };

        return Ok(response);
    }
}