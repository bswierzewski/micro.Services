using Bogus;
using Database.Entities.DeviceInfo;
using JsonDbCreator.StaticData;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace JsonDbCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;

            var categoryFaker = new Faker<Category>()
                .RuleFor(x => x.Id, f => counter++)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.IconName, f => f.PickRandom(Icons.MaterialDesign));

            var categories = categoryFaker.Generate(10);
            counter = 0;

            var componentFaker = new Faker<Component>()
                .RuleFor(x => x.Id, f => counter++)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.IconName, f => f.PickRandom(Icons.MaterialDesign));

            var components = componentFaker.Generate(20);
            counter = 0;

            var kindFaker = new Faker<Kind>()
                .RuleFor(x => x.Id, f => counter++)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.PhotoUrl, f => f.Image.PicsumUrl());

            var kinds = kindFaker.Generate(10);
            counter = 0;

            var json = JsonConvert.SerializeObject(new
            {
                categories = categories,
                components = components,
                kinds = kinds
            }, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });

            File.WriteAllText(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "db.json"), json);

            //Console.WriteLine(json);
        }
    }
}
