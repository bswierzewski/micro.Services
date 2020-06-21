using DeviceService.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<DeviceKind> DeviceTypes { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Registration> Registrations { get; set; }
    }
}