using Company.Identity.Application.User.Commands;
using Company.Identity.Application.User.Interfaces.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Company.Identity.Api.User.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(ICreateUserHandler createUserHandler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        await createUserHandler.HandleAsync(command);
        return Ok();
    }
}