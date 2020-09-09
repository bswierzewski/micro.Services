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
                .RuleFor(x => x.Id, f => categoryCounter++)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.IconName, f => f.PickRandom(Icons.FontAwesome));

            var categories = categoryFaker.Generate(10);

            int componentCounter = 0;

            var componentFaker = new Faker<Component>()
                .RuleFor(x => x.Id, f => componentCounter++)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.IconName, f => f.PickRandom(Icons.FontAwesome));

            var components = componentFaker.Generate(20);

            int kindCounter = 0;

            var kindFaker = new Faker<Kind>()
                .RuleFor(x => x.Id, f => kindCounter++)
                .RuleFor(x => x.Created, f => f.Date.Soon())
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.PhotoUrl, f => f.Image.PicsumUrl());

            var kinds = kindFaker.Generate(10);



            var json = JsonConvert.SerializeObject(new
            {
                categories = categories,
                components = components,
                kinds = kinds
            }, Formatting.Indented);

            File.WriteAllText("db.json", json);

            //Console.WriteLine(json);
        }
    }
}
