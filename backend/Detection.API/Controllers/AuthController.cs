using Detection.API.Models;
using Detection.API.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace Detection.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("AllowSites")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IFingerPrintService _fingerPrintService;

    public AuthController(IUserService userService,
        IFingerPrintService fingerPrintService)
    {
        _userService = userService;
        _fingerPrintService = fingerPrintService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthModel loingModel)
    {
        if (loingModel == null || string.IsNullOrEmpty(loingModel.Email) || string.IsNullOrEmpty(loingModel.Password))
        {
            return BadRequest("Invalid login request.");
        }

        // Simulate user authentication (You should check against your user database)

        var fingerprint = _fingerPrintService.GetDeviceFingerprint(loingModel);

        if (fingerprint is null)
        {
            var user = _userService.Authenticate(loingModel.Email, loingModel.Password);

            if (user is null)
            {
                return Unauthorized("Invalid credentials.");
            }

            fingerprint = _fingerPrintService.AddFingerPrint(loingModel);
        }
        else
        {
            Console.WriteLine($"{loingModel.Email} are already login from a devices : {JsonSerializer.Serialize(fingerprint)}");
            return BadRequest($"You are already login from a devices : {JsonSerializer.Serialize(fingerprint)}");
        }


        return Ok(new { message = "Login successful!" });
    }


    // Register action
    [HttpPost("register")]
    public IActionResult Register([FromBody] AuthModel registerModel)
    {
        if (registerModel == null || string.IsNullOrEmpty(registerModel.Email) || string.IsNullOrEmpty(registerModel.Password))
        {
            return BadRequest("Email and password are required.");
        }

        // You could add additional validation here (e.g., check if the email already exists)

        var existingUser = _userService.GetUserByEmail(registerModel.Email);
        if (existingUser != null)
        {
            return BadRequest("User with this email already exists.");
        }

        // Create the user and save it
        var newUser = new User
        {
            Email = registerModel.Email,
            Password = registerModel.Password, // Hash password before saving in a real application
        };

        _userService.RegisterUser(newUser);

        return Ok(new { message = "Registration successful!" });
    }


    // logout action
    [HttpPost("logout")]
    public IActionResult Logout([FromBody] AuthModel authModel)
    {
        if (authModel == null || string.IsNullOrEmpty(authModel.Email) || string.IsNullOrEmpty(authModel.Password))
        {
            return BadRequest("Bad request from frontend");
        }

        var fingerPrintInfo = _fingerPrintService.RemoveFingerPrint(authModel);

        if (fingerPrintInfo is not null)
        {
            return Ok(fingerPrintInfo);
        }

        return BadRequest("GANDU");
    }
}
