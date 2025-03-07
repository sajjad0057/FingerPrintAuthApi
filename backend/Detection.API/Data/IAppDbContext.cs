using Detection.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Detection.API.Data;

public interface IAppDbContext
{
    DbSet<DeviceFingerprint> DeviceFingerprints { get; }
    DbSet<User> Users { get; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}