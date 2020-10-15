﻿using Database.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Database
{
    public static class Seed
    {
        public static void SeedTestUser(DataContext context)
        {
            if (!context.Users.Any())
            {
                var user = new User()
                {
                    Created = DateTime.Now,
                    IsActive = true,
                    LastActive = DateTime.Now,
                    Username = "test"
                };

                user.Id = 0;
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("test", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();

                context.Users.Add(user);
            };

            context.SaveChanges();
        }


        public static void SeedData(DataContext context)
        {
            var jsonString = File.ReadAllText(Path.Combine(System.Reflection.Assembly.GetAssembly(typeof(Seed)).Location, "../SeedData/Json/db.json"));

            var jsonObject = JObject.Parse(jsonString);

            if (!context.Users.Any())
            {
                var users = DeserializeJsonObject<User>(jsonObject, "users");
                users.ForEach(user =>
                {
                    user.Id = 0;
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();

                    context.Users.Add(user);
                });

                context.SaveChanges();
            }

            if (!context.Kinds.Any())
            {
                var kinds = DeserializeJsonObject<Kind>(jsonObject, "kinds");
                kinds.ForEach(kind =>
                {
                    kind.Id = 0;
                    context.Kinds.Add(kind);
                });

                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                var categories = DeserializeJsonObject<Category>(jsonObject, "categories");
                categories.ForEach(category =>
                {
                    category.Id = 0;
                    context.Categories.Add(category);
                });

                context.SaveChanges();
            }

            if (!context.Components.Any())
            {
                var components = DeserializeJsonObject<Component>(jsonObject, "components");
                components.ForEach(component =>
                {
                    component.Id = 0;
                    context.Components.Add(component);
                });

                context.SaveChanges();
            }

            if (!context.Addresses.Any())
            {
                var addresses = DeserializeJsonObject<Address>(jsonObject, "addresses");
                addresses.ForEach(address =>
                {
                    address.Id = 0;
                    context.Addresses.Add(address);
                });

                context.SaveChanges();
            }

            if (!context.FileDatas.Any())
            {
                var fileDatas = DeserializeJsonObject<FileData>(jsonObject, "filesData");
                fileDatas.ForEach(fileData =>
                {
                    fileData.Id = 0;
                    context.FileDatas.Add(fileData);
                });

                context.SaveChanges();
            }

            if (!context.Versions.Any())
            {
                var versions = DeserializeJsonObject<Database.Entities.Version>(jsonObject, "versions");
                versions.ForEach(version =>
                {
                    version.Id = 0;
                    context.Versions.Add(version);
                });

                context.SaveChanges();
            }

            if (!context.Devices.Any())
            {
                var devices = DeserializeJsonObject<Device>(jsonObject, "devices");
                devices.ForEach(device =>
                {
                    device.Id = 0;
                    context.Devices.Add(device);
                });

                context.SaveChanges();
            }

            if (!context.Registrations.Any())
            {
                var registrations = DeserializeJsonObject<Registration>(jsonObject, "registrations");
                registrations.ForEach(registration =>
                {
                    registration.Id = 0;
                    context.Registrations.Add(registration);
                });

                context.SaveChanges();
            }
        }

        #region Helper Seed Methods

        private static List<T> DeserializeJsonObject<T>(JObject jsonObject, string collectionName)
        {
            List<T> resultList = null;

            if (jsonObject[collectionName] != null)
                resultList = jsonObject[collectionName].ToObject<List<T>>();

            return resultList;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        #endregion
    }
}
