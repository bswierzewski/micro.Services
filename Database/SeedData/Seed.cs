using Database.Entities;
using Newtonsoft.Json;
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

            if (!context.DeviceKinds.Any())
            {
                var deviceKindData = File.ReadAllText(Path.Combine(pathToJson, "DeviceKind.json"));
                var deviceKinds = JsonConvert.DeserializeObject<List<DeviceKind>>(deviceKindData);
                foreach (var deviceKind in deviceKinds)
                {
                    context.DeviceKinds.Add(deviceKind);
                }
                context.SaveChanges();
            }

            if (!context.DeviceTypes.Any())
            {
                var deviceTypeData = File.ReadAllText(Path.Combine(pathToJson, "DeviceType.json"));
                var deviceTypes = JsonConvert.DeserializeObject<List<DeviceType>>(deviceTypeData);
                foreach (var deviceType in deviceTypes)
                {
                    context.DeviceTypes.Add(deviceType);
                }
                context.SaveChanges();
            }

            if (!context.Devices.Any())
            {
                var deviceData = File.ReadAllText(Path.Combine(pathToJson, "Device.json"));
                var devices = JsonConvert.DeserializeObject<List<Database.Entities.Device>>(deviceData);
                foreach (var device in devices)
                {
                    context.Devices.Add(device);
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

        #endregion
    }
}
