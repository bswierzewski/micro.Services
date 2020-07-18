using Database.Entities;
using Database.Entities.DeviceInfo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Database
{
    public static class Seed
    {
        public static void SeedData(DataContext context)
        {
            var pathToJson = Path.Combine(System.Reflection.Assembly.GetAssembly(typeof(Seed)).Location, "../SeedData/Json/");

            if (!context.Users.Any())
            {
                var userData = File.ReadAllText(Path.Combine(pathToJson, "User.json"));
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();

                    context.Users.Add(user);
                }

                context.SaveChanges();
            }

            if (!context.FileDatas.Any())
            {
                var fileData = File.ReadAllText(Path.Combine(pathToJson, "FileData.json"));
                var fileDatas = JsonConvert.DeserializeObject<List<FileData>>(fileData);
                foreach (var file in fileDatas)
                {
                    file.Content = GetRandomByteArray();
                    file.Created = DateTime.Now;

                    context.FileDatas.Add(file);
                }
                context.SaveChanges();
            }

            if (!context.Versions.Any())
            {
                var versionData = File.ReadAllText(Path.Combine(pathToJson, "Version.json"));
                var versions = JsonConvert.DeserializeObject<List<Entities.Version>>(versionData);
                foreach (var version in versions)
                {
                    version.Created = DateTime.Now;
                    context.Versions.Add(version);
                }
                context.SaveChanges();
            }

            if (!context.Devices.Any())
            {
                var deviceData = File.ReadAllText(Path.Combine(pathToJson, "Device.json"));
                var devices = JsonConvert.DeserializeObject<List<Device>>(deviceData);
                foreach (var device in devices)
                {
                    device.Created = DateTime.Now;
                    context.Devices.Add(device);
                }
                context.SaveChanges();
            }

            if (!context.Kinds.Any())
            {
                var kindData = File.ReadAllText(Path.Combine(pathToJson, "Kind.json"));
                var kinds = JsonConvert.DeserializeObject<List<Kind>>(kindData);
                foreach (var kind in kinds)
                {
                    kind.Created = DateTime.Now;
                    context.Kinds.Add(kind);
                }
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                var categoryData = File.ReadAllText(Path.Combine(pathToJson, "Category.json"));
                var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                foreach (var category in categories)
                {
                    category.Created = DateTime.Now;
                    context.Categories.Add(category);
                }
                context.SaveChanges();
            }

            if (!context.Components.Any())
            {
                var componentData = File.ReadAllText(Path.Combine(pathToJson, "Component.json"));
                var components = JsonConvert.DeserializeObject<List<Component>>(componentData);
                foreach (var component in components)
                {
                    component.Created = DateTime.Now;
                    context.Components.Add(component);
                }
                context.SaveChanges();
            }
        }

        #region Helper Seed Methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static byte[] GetRandomByteArray()
        {
            Random rnd = new Random();
            var b = new byte[10];

            rnd.NextBytes(b);

            return b;
        }

        #endregion
    }
}
