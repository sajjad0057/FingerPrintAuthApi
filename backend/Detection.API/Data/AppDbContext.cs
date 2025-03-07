using Detection.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Detection.API.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<DeviceFingerprint> DeviceFingerprints => Set<DeviceFingerprint>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
    }
}