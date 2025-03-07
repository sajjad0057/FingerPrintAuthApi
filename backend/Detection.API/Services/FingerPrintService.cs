using Detection.API.Data;
using Detection.API.Models;

namespace Detection.API.Services;

public class FingerPrintService : IFingerPrintService
{
    private readonly IAppDbContext _context;
    public FingerPrintService(IAppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    public DeviceFingerprint? AddFingerPrint(AuthModel authInfo)
    {
        DeviceFingerprint deviceFingerprint = new();
        if (authInfo is not null)
        {
            deviceFingerprint.UserEmail = authInfo.Email;
            deviceFingerprint.Fingerprint = authInfo.Fingerprint;
            deviceFingerprint.Datetime = DateTime.UtcNow;

            _context.DeviceFingerprints.Add(deviceFingerprint);
            _context.SaveChanges();
        }

        return deviceFingerprint;
    }

    public DeviceFingerprint? GetDeviceFingerprint(AuthModel authInfo)
    {
        return _context.DeviceFingerprints.FirstOrDefault(x => x.Fingerprint == authInfo.Fingerprint
            || x.UserEmail == authInfo.Email);
    }

    public DeviceFingerprint? RemoveFingerPrint(AuthModel authInfo)
    {
        DeviceFingerprint? deviceFingerprint = null;

        if (authInfo is not null)
        {
            deviceFingerprint = GetDeviceFingerprint(authInfo);
            _context.DeviceFingerprints.Remove(deviceFingerprint);
            _context.SaveChanges();
        }

        return deviceFingerprint;
    }
}
