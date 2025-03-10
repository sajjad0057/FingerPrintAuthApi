﻿namespace Detection.API.Models;

public class AuthModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Fingerprint { get; set; } // Unique fingerprint ID
}
