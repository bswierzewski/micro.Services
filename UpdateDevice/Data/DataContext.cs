using UpdateDeviceService.Models;
using Microsoft.EntityFrameworkCore;

namespace UpdateDeviceService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeviceVersion>()   
                .HasKey(x => new { x.DeviceId, x.VersionId });
        }

        public DbSet<Version> Versions { get; set; }
        public DbSet<FileData> FileDatas { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceVersion> DeviceVersions { get; set; }
    }
}