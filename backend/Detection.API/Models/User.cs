namespace Detection.API.Models;

public class User
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Fingerprint { get; set; } // Store the fingerprint or device info
}
