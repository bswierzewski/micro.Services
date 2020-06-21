using Database;
using Database.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Device.Data
{
    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            if (!context.DeviceTypes.Any())
            {
                var deviceTypeData = System.IO.File.ReadAllText("Data/SeedData/DeviceTypeSeedData.json");
                var deviceTypes = JsonConvert.DeserializeObject<List<DeviceKind>>(deviceTypeData);
                foreach (var deviceType in deviceTypes)
                {
                    context.DeviceTypes.Add(deviceType);
                }
                context.SaveChanges();
            }

            if (!context.Devices.Any())
            {
                var deviceData = System.IO.File.ReadAllText("Data/SeedData/DeviceSeedData.json");
                var devices = JsonConvert.DeserializeObject<List<Database.Entities.Device>>(deviceData);
                foreach (var device in devices)
                {
                    context.Devices.Add(device);
                }
                context.SaveChanges();
            }
        }
    }
}