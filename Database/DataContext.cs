using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database
{
    public class DataContext : DbContext
    {
        public static Action<DbContextOptionsBuilder> DbContextBuilder = x => x.UseSqlite("local.db");
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeviceVersion>()
                .HasKey(x => new { x.DeviceId, x.VersionId });
        }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceVersion> DeviceVersions { get; set; }
        public DbSet<DeviceKind> DeviceTypes { get; set; }
        public DbSet<FileData> FileDatas { get; set; }
        public DbSet<Entities.Version> Versions { get; set; }
    }
}
