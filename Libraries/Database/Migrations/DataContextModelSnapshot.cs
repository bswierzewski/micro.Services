﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Database.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Database.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Label")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Label")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Database.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Database.Entities.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("Database.Entities.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<int?>("ComponentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<bool?>("IsAutoUpdate")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsUpdated")
                        .HasColumnType("boolean");

                    b.Property<int?>("KindId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("VersionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("CategoryId");

                    b.HasIndex("ComponentId");

                    b.HasIndex("KindId");

                    b.HasIndex("VersionId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Database.Entities.FileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("Content")
                        .HasColumnType("bytea");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Extension")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FileDatas");
                });

            modelBuilder.Entity("Database.Entities.Kind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Kinds");
                });

            modelBuilder.Entity("Database.Entities.Registration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BleAddressId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MacAddressId")
                        .HasColumnType("integer");

                    b.Property<int>("Rssi")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BleAddressId");

                    b.HasIndex("MacAddressId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("Database.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Database.Entities.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Database.Entities.Version", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("ComponentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("FileDataId")
                        .HasColumnType("integer");

                    b.Property<int?>("KindId")
                        .HasColumnType("integer");

                    b.Property<short>("Major")
                        .HasColumnType("smallint");

                    b.Property<short>("Minor")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<short>("Patch")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("FileDataId");

                    b.HasIndex("KindId");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Database.Entities.Component", b =>
                {
                    b.HasOne("Database.Entities.Category", "Category")
                        .WithMany("Components")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("Database.Entities.Device", b =>
                {
                    b.HasOne("Database.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("Database.Entities.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId");

                    b.HasOne("Database.Entities.Kind", "Kind")
                        .WithMany()
                        .HasForeignKey("KindId");

                    b.HasOne("Database.Entities.Version", "Version")
                        .WithMany()
                        .HasForeignKey("VersionId");
                });

            modelBuilder.Entity("Database.Entities.Registration", b =>
                {
                    b.HasOne("Database.Entities.Address", "BleAddress")
                        .WithMany()
                        .HasForeignKey("BleAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.Address", "MacAddress")
                        .WithMany()
                        .HasForeignKey("MacAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Entities.UserRole", b =>
                {
                    b.HasOne("Database.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Entities.Version", b =>
                {
                    b.HasOne("Database.Entities.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId");

                    b.HasOne("Database.Entities.FileData", "FileData")
                        .WithMany()
                        .HasForeignKey("FileDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.Kind", "Kind")
                        .WithMany()
                        .HasForeignKey("KindId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Database.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Database.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Database.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Database.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
