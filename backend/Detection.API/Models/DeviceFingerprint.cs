namespace Detection.API.Models;

public class DeviceFingerprint
{
    public int Id { get; set; }
    public string UserEmail { get; set; }
    public string Fingerprint { get; set; }
    public DateTime Datetime { get; set; }
}
