﻿using Database.Entities;
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
                                     "Server=db;" +
                                     "Database=micro");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeviceVersion>()
                .HasKey(x => new { x.DeviceId, x.VersionId });

            modelBuilder.Entity<DeviceType>()
                .HasIndex(x => x.Type)
                .IsUnique();
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<DeviceVersion> DeviceVersions { get; set; }
        public DbSet<FileData> FileDatas { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Version> Versions { get; set; }
    }
}
