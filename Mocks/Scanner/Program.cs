using Bogus;
using Scanner.Model;
using Scanner.Static;
using StackExchange.Redis;
using System;
using System.Threading;

namespace Scanner
{
    class Program
    {
        static void Main(string[] args)
        {
            var fakerScan = new Faker<RedisValueModel>()
                .RuleFor(x => x.time, f => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .RuleFor(x => x.name, f => f.PickRandom(Names))
                .RuleFor(x => x.macAddress, f => f.PickRandom(MacAddress))
                .RuleFor(x => x.rssi, f => f.Random.Int(-100, -50));

            ConnectionMultiplexer redisConn = ConnectionMultiplexer.Connect(Urls.ConnectionRedis);

            IDatabase db = redisConn.GetDatabase();

            while (true)
            {
                RedisValueModel redisValue = fakerScan.Generate(1)[0];

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(redisValue);

                Console.WriteLine(json);

                db.Publish(redisValue.name, json);

                db.ListRightPushAsync("micro", json);

                Thread.Sleep(1250);
            }
        }

        static string[] Names =
        {
            "52:eb:6c:61:42:fb","d5:a5:c3:41:12:54","6f:6d:71:e8:14:f6","ac:3d:47:b5:63:31","1c:63:89:a4:ac:d4","e9:91:9a:a2:be:dd",
            "a4:d5:90:cc:f6:c5","95:d2:42:9b:50:dc","d1:9e:c2:65:da:40","48:9d:87:3b:44:df","71:30:f9:ee:57:81","b9:cc:af:93:44:3f",
            "66:e0:46:81:1f:90","ef:81:12:ad:d2:0d","11:84:b6:5a:5a:20","55:69:90:b0:5a:c5","cc:a6:7b:c1:e1:83","2c:3d:5a:83:fe:fa",
            "f9:c6:7e:f7:4b:c3","cd:c4:5e:eb:dd:d2",
        };

        static string[] MacAddress =
        {
            "e1:17:26:67:e3:ea","3e:2b:66:a5:18:0b","9c:90:6c:d3:c5:77","2d:54:44:73:ae:fd","91:ab:90:83:d4:3a","e1:54:6d:61:b8:08",
            "e3:ec:15:a3:fa:53","92:b8:bd:1e:00:1e","5b:e8:7b:2a:f7:dd","a2:6e:68:28:d5:38","5a:ba:57:2c:c3:c3","9e:3b:d7:63:f0:d9",
            "62:8a:59:f1:e1:f6","40:73:aa:72:28:eb","be:b6:5d:8f:be:4a","a0:ba:17:dc:48:40","fb:65:f4:f9:7f:c5","46:f8:f8:a2:c8:6c",
            "83:b1:8d:72:e8:62","8d:e8:92:6b:6d:0d","66:0e:7d:8d:c3:36","c1:85:41:19:f5:7b","01:47:f3:e5:2b:2f","f3:1c:ee:f0:0b:0d",
            "bc:2d:d1:b4:4f:60","ad:90:f8:e7:92:71","47:b9:34:c1:e0:42","2b:6a:bb:12:70:a1","6e:a8:bc:62:77:50","1e:76:02:70:d0:95",
            "64:0d:36:e0:6b:65","83:19:b4:82:9d:72","fa:31:3c:ab:c4:a1","51:aa:bd:30:46:a7","a3:4a:18:a7:c8:1c","6e:d7:e8:9f:5d:d3",
            "0b:4b:fe:56:8f:56","ec:eb:0a:4a:67:46","14:73:b1:61:1e:26","d8:34:c6:8f:3c:21",
        };
    }
}
