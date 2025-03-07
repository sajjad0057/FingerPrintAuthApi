using Detection.API.Models;

namespace Detection.API.Services
{
    public interface IFingerPrintService
    {
        DeviceFingerprint? AddFingerPrint(AuthModel authInfo);
        DeviceFingerprint? GetDeviceFingerprint(AuthModel authInfo);
        DeviceFingerprint? RemoveFingerPrint(AuthModel authInfo);
    }
}