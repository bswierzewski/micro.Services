using Database.Entities;
using Database.Entities.DeviceInfo;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DataContext : DbContext
    {
        public DataContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql("Username=postgres;" +
                                     "Password=mysecretpassword;" +
                                     "Server=localhost;" +
                                     "Database=micro");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DeviceComponent> DeviceComponents { get; set; }
        public DbSet<FileData> FileDatas { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Version> Versions { get; set; }
    }
}
