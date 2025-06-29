using AutoMapper;
using Company.Identity.Api.User.Requests;
using Company.Identity.Api.User.Responses;
using Company.Identity.Application.User.Commands;
using Company.Identity.Application.User.Interfaces.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Company.Identity.Api.User.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(ICreateUserHandler createUserHandler, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var command = mapper.Map<CreateUserCommand>(request);
        var result = await createUserHandler.HandleAsync(command);

        if (!result.IsSuccess)
            return BadRequest(result);

        var response = mapper.Map<CreateUserResponse>(result.Value);
        return Ok(response);
    }
}