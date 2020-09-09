using Bogus;
using Database.Entities;
using Database.Entities.DeviceInfo;
using JsonDbCreator.StaticData;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace JsonDbCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            int categoryCounter = 0;

            var categoryFaker = new Faker<Category>()
                .RuleFor(x => x.Id, x => categoryCounter++)
                .RuleFor(x => x.Created, x => x.Date.Soon())
                .RuleFor(x => x.Name, x => x.Name.FirstName())
                .RuleFor(x => x.FontAwesome, x => x.PickRandom(Icons.FontAwesome));

            var categories = categoryFaker.Generate(2);

            int componentCounter = 0;

            var componentFaker = new Faker<Component>()
                .RuleFor(x => x.Id, x => componentCounter++)
                .RuleFor(x => x.Created, x => x.Date.Soon())
                .RuleFor(x => x.Name, x => x.Name.FirstName())
                .RuleFor(x => x.FontAwesome, x => x.PickRandom(Icons.FontAwesome));

            var components = componentFaker.Generate(10);

            var json = JsonConvert.SerializeObject(new
            {
                categories = categories,
                components = components
            }, Formatting.Indented);

            //File.WriteAllText("db.json", json);

            Console.WriteLine(json);
        }
    }
}
