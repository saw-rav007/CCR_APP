using CCR.Application.DTOs;
using CCR.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CCR.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        try
        {
            var user = await _userService.RegisterUserAsync(request);
            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
