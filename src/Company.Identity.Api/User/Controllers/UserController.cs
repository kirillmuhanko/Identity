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

        await createUserHandler.HandleAsync(command);

        var response = new CreateUserResponse
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            Email = request.Email
        };

        return Ok(response);
    }
}