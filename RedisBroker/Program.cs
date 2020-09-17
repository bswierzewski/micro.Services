using Broker.Model;
using Broker.Service;
using Broker.Static;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Broker
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisChannel key2 = "micro";
            RedisValue value;

            List<string> names = new List<string> { { "Mays" }, { "Bass" }, { "Hinton" }, { "Cassie" } };

            List<string> adresses = new List<string> { { "AB:F8:E6:48:C2:C8" }, { "6D:99:D8:46:09:3A" }, { "01:43:B8:65:35:D1" }, { "C1:98:35:09:29:2F" } };

            Random rnd = new Random();

            ConnectionMultiplexer redisConn = ConnectionMultiplexer.Connect(Urls.ConnectionRedis);

            IDatabase db = redisConn.GetDatabase();

            while (true)
            {
                key2 = names.ElementAt(rnd.Next(0, 4));

                value = $"{{\"time\": \"{DateTime.Now}\",\"name\": \"{key2}\",\"address\": \"{adresses.ElementAt(rnd.Next(0, 4))}\",\"rssi\":\"{rnd.Next(50, 100)}\" }}";

                db.Publish(key2, value);

                Thread.Sleep(250);
            }
        }
    }
    class Redis
    {
        private readonly IBrokerService _service;

        public Redis(IBrokerService service)
        {
            _service = service;
        }
        void RedisWorker()
        {

            List<RedisValue> values = new List<RedisValue>();
            RedisKey key = "micro";
            RedisValue value;

            ConnectionMultiplexer redisConn = ConnectionMultiplexer.Connect(Urls.ConnectionRedis);

            IDatabase db = redisConn.GetDatabase();

            while (true)
            {
                while ((value = db.ListRightPop(key)).HasValue)
                {
                    Console.WriteLine(value);

                    values.Add(value);

                    if (values.Count > 30)
                    {
                        var jsonValues = DeserializeValues(values);

                        _service.SaveValues(jsonValues);

                        _service.AddToTempDevices(jsonValues);

                        values.Clear();
                    }
                }

                Thread.Sleep(500);
            }
        }

        private List<RedisValueModel> DeserializeValues(List<RedisValue> values)
        {
            var result = new List<RedisValueModel>();

            values.ForEach(value =>
            {
                result.Add(JsonConvert.DeserializeObject<RedisValueModel>(value));
            });

            return result;
        }
    }
}