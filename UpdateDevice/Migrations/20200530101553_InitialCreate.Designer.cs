﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UpdateDeviceService.Data;

namespace UpdateDeviceService.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200530101553_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("UpdateDeviceService.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MacAddress")
                        .HasColumnType("TEXT");

                    b.Property<int?>("VersionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VersionId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("UpdateDeviceService.Models.DeviceVersion", b =>
                {
                    b.Property<int>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VersionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DeviceId", "VersionId");

                    b.HasIndex("VersionId");

                    b.ToTable("DeviceVersions");
                });

            modelBuilder.Entity("UpdateDeviceService.Models.FileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Content")
                        .HasColumnType("BLOB");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileExtension")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FileDatas");
                });

            modelBuilder.Entity("UpdateDeviceService.Models.Version", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FileDataId")
                        .HasColumnType("INTEGER");

                    b.Property<short>("Major")
                        .HasColumnType("INTEGER");

                    b.Property<short>("Minor")
                        .HasColumnType("INTEGER");

                    b.Property<short>("Patch")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FileDataId");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("UpdateDeviceService.Models.Device", b =>
                {
                    b.HasOne("UpdateDeviceService.Models.Version", "Version")
                        .WithMany()
                        .HasForeignKey("VersionId");
                });

            modelBuilder.Entity("UpdateDeviceService.Models.DeviceVersion", b =>
                {
                    b.HasOne("UpdateDeviceService.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UpdateDeviceService.Models.Version", "Version")
                        .WithMany()
                        .HasForeignKey("VersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UpdateDeviceService.Models.Version", b =>
                {
                    b.HasOne("UpdateDeviceService.Models.FileData", "FileData")
                        .WithMany()
                        .HasForeignKey("FileDataId");
                });
#pragma warning restore 612, 618
        }
    }
}
