using Bogus;
using Database.Entities;
using JsonDb.StaticData;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;

namespace JsonDb
{
    class Program
    {
        public static string JsonPath => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        static void Main(string[] args)
        {
            var categoryFaker = new Faker<Category>()
                .RuleFor(x => x.Id, f => f.IndexFaker + 1)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.Icon, f => f.PickRandom(Icons.MaterialDesign));

            var categories = categoryFaker.Generate(10);

            int[] categoryIds = categories.Select(x => x.Id).ToArray();

            var componentFaker = new Faker<Component>()
                .RuleFor(x => x.Id, f => f.IndexFaker + 1)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.Icon, f => f.PickRandom(Icons.MaterialDesign))
                .RuleFor(x => x.CategoryId, f => f.PickRandom(categoryIds));

            var components = componentFaker.Generate(20);

            var kindFaker = new Faker<Kind>()
                .RuleFor(x => x.Id, f => f.IndexFaker + 1)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Icon, f => f.PickRandom(Icons.MaterialDesign))
                .RuleFor(x => x.Name, f => f.Vehicle.Type());

            var kinds = kindFaker.Generate(10);

            var addressFaker = new Faker<Address>()
                .RuleFor(x => x.Id, f => f.IndexFaker + 1)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.IsConfirmed, f => f.Random.Bool())
                .RuleFor(x => x.Label, f => f.Internet.Mac());

            var addresses = addressFaker.Generate(50);

            int[] componentIds = components.Select(x => x.Id).ToArray();
            int[] kindIds = kinds.Select(x => x.Id).ToArray();
            int[] addressIds = addresses.Select(x => x.Id).ToArray();

            var deviceFaker = new Faker<Device>()
                .RuleFor(x => x.Id, f => f.IndexFaker + 1)
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.IsAutoUpdate, f => f.Random.Bool())
                .RuleFor(x => x.KindId, f => f.PickRandom(kindIds))
                .RuleFor(x => x.ComponentId, f => f.IndexVariable = f.PickRandom(componentIds))
                .RuleFor(x => x.CategoryId, f => components[f.IndexVariable - 1].CategoryId)
                .RuleFor(x => x.AddressId, f => addressIds[f.IndexFaker])
                .RuleFor(x => x.Icon, f => f.PickRandom(Icons.MaterialDesign));

            var devices = deviceFaker.Generate(30);

            var fileDataFaker = new Faker<FileData>()
                .RuleFor(x => x.Id, f => f.IndexFaker + 1)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Internet.UserName())
                .RuleFor(x => x.Extension, f => f.System.FileExt())
                .RuleFor(x => x.Content, f => f.Random.Bytes(30));

            var filesData = fileDataFaker.Generate(10);

            int[] fileDataIds = filesData.Select(x => x.Id).ToArray();

            var versionFaker = new Faker<Database.Entities.Version>()
                .RuleFor(x => x.Id, f => f.IndexFaker + 1)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.JobType())
                .RuleFor(x => x.Major, f => (short)++f.IndexVariable)
                .RuleFor(x => x.Minor, f => (short)++f.IndexVariable)
                .RuleFor(x => x.Patch, f => (short)++f.IndexVariable)
                .RuleFor(x => x.KindId, f => f.PickRandom(kindIds))
                .RuleFor(x => x.ComponentId, f => f.PickRandom(componentIds))
                .RuleFor(x => x.FileDataId, f => f.PickRandom(fileDataIds));

            var versions = versionFaker.Generate(5);

            var userFaker = new Faker<User>()
                .RuleFor(x => x.Id, f => f.IndexFaker + 1)
                .RuleFor(x => x.Username, f => f.Internet.UserName())
                .RuleFor(x => x.Created, f => f.Date.Past())
                .RuleFor(x => x.LastActive, f => f.Date.Past())
                .RuleFor(x => x.IsActive, f => true);

            var users = userFaker.Generate(10);

            var registrationFaker = new Faker<Registration>()
                .RuleFor(x => x.Id, f => f.IndexFaker + 1)
                .RuleFor(x => x.Created, f => f.Date.Soon(-1))
                .RuleFor(x => x.MacAddressId, f => f.PickRandom(addressIds))
                .RuleFor(x => x.BleAddressId, f => f.PickRandom(addressIds))
                .RuleFor(x => x.Rssi, f => f.Random.Int(-120, -50));

            var registrations = registrationFaker.Generate(100);

            var json = JsonConvert.SerializeObject(new
            {
                categories,
                components,
                kinds,
                devices,
                filesData,
                versions,
                users,
                addresses,
                registrations
            }, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });

            File.WriteAllText(Path.Join(JsonPath, "db.json"), json);

            //Console.WriteLine(json);
        }
    }
}
