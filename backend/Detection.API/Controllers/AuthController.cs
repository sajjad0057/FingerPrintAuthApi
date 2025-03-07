using Detection.API.Models;
using Detection.API.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace Detection.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("AllowSites")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel loginModel)
    {
        if (loginModel == null || string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
        {
            return BadRequest("Invalid login request.");
        }

        // Simulate user authentication (You should check against your user database)
        var user = _userService.Authenticate(loginModel.Email, loginModel.Password);

        if (user == null)
        {
            return Unauthorized("Invalid credentials.");
        }

        // Check if the fingerprint matches (you can store fingerprints and check them)
        if (!string.IsNullOrEmpty(user.Fingerprint) && user.Fingerprint != loginModel.Fingerprint)
        {
            return Unauthorized("Fingerprint mismatch.");
        }

        return Ok(new { message = "Login successful!" });
    }
}
