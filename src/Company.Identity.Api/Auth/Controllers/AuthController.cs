using AutoMapper;
using Company.Identity.Api.Auth.Requests;
using Company.Identity.Api.Auth.Responses;
using Company.Identity.Application.Auth.Commands;
using Company.Identity.Application.Auth.Interfaces.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Company.Identity.Api.Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ICreateUserHandler createUserHandler, IMapper mapper) : ControllerBase
{
    [HttpPost("create-user")]
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