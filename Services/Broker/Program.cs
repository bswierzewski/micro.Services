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