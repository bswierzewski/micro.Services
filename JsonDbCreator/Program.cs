using Bogus;
using Database.Entities;
using Database.Entities.DeviceInfo;
using JsonDbCreator.StaticData;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonDbCreator
{
    class Program
    {
        public static string JsonPath => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        static void Main(string[] args)
        {
            var categoryFaker = new Faker<Category>()
                .RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.IconName, f => f.PickRandom(Icons.MaterialDesign));

            var categories = categoryFaker.Generate(10);

            var componentFaker = new Faker<DeviceComponent>()
                .RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.IconName, f => f.PickRandom(Icons.MaterialDesign));

            var components = componentFaker.Generate(20);

            var kindFaker = new Faker<Kind>()
                .RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.PhotoUrl, f => f.Image.PicsumUrl());

            var kinds = kindFaker.Generate(10);

            int[] componentIds = components.Select(x => x.Id).ToArray();
            int[] kindIds = kinds.Select(x => x.Id).ToArray();

            var deviceFaker = new Faker<Device>()
                .RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.MacAddress, f => f.Internet.Mac())
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.PhotoUrl, f => f.Image.PicsumUrl())
                .RuleFor(x => x.IsAutoUpdate, f => f.Random.Bool())
                .RuleFor(x => x.KindId, f => f.PickRandom(kindIds))
                .RuleFor(x => x.DeviceComponentId, f => f.PickRandom(componentIds));

            var devices = deviceFaker.Generate(30);

            var fileDataFaker = new Faker<FileData>()
                .RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Internet.UserName())
                .RuleFor(x => x.Extension, f => f.PickRandom(new string[] { "exe", "dll" }))
                .RuleFor(x => x.Content, f => f.Random.Bytes(30));

            var filesData = fileDataFaker.Generate(10);

            int[] fileDataIds = filesData.Select(x => x.Id).ToArray();

            var versionFaker = new Faker<Database.Entities.Version>()
                .RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.JobType())
                .RuleFor(x => x.Major, f => (short)f.IndexVariable++)
                .RuleFor(x => x.Minor, f => (short)f.IndexVariable++)
                .RuleFor(x => x.Patch, f => (short)f.IndexVariable++)
                .RuleFor(x => x.KindId, f => f.PickRandom(kindIds))
                .RuleFor(x => x.DeviceComponentId, f => f.PickRandom(componentIds))
                .RuleFor(x => x.FileDataId, f => f.PickRandom(fileDataIds));

            var versions = versionFaker.Generate(5);

            var json = JsonConvert.SerializeObject(new
            {
                categories = categories,
                components = components,
                kinds = kinds,
                devices = devices,
                filesData = filesData,
                versions = versions,
            }, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });

            //File.WriteAllText(Path.Join(JsonPath, "db.json"), json);


            // ------------------------

            CreateSeparateFile(categories, "Category");
            CreateSeparateFile(components, "Component");
            CreateSeparateFile(devices, "Device");
            CreateSeparateFile(filesData, "FileData");
            CreateSeparateFile(kinds, "Kind");
            CreateSeparateFile(versions, "Version");

            // ------------------------


            //Console.WriteLine(json);
        }

        private static void CreateSeparateFile(object obj, string fileName)
        {
            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });

            string seedDataPath = Path.Join(JsonPath, "SeedData");

            if (!Directory.Exists(seedDataPath))
                Directory.CreateDirectory(seedDataPath);

            File.WriteAllText(Path.Join(seedDataPath, $"{fileName}.json"), json);
        }
    }
}
