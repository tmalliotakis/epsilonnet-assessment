using EpsilonWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EpsilonWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService auth) : ControllerBase
    {
        // POST api/auth/login -> returns a JWT for API clients.
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (!auth.ValidateCredentials(request.Username, request.Password))
                return Unauthorized(new { message = "Invalid username or password." });

            var token = auth.GenerateJwt(request.Username);
            return Ok(new { token });
        }
    }

    public record LoginRequest(string Username, string Password);
}
