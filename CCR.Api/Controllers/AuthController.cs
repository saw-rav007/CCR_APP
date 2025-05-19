using CCR.Application.DTOs;
using CCR.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CCR.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;   
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult GetUserByEmail()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(userId);
        }

        [Authorize]
        [HttpGet("token-status")]
        public IActionResult TokenStatus()
        {
            var expiry = User.FindFirst("exp")?.Value ?? throw new ArgumentNullException("exp cannot be null.");
            var expTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry));
            return Ok(new { ExpiresAt = expTime, UtcNow = DateTime.UtcNow });
        }


    }
}
