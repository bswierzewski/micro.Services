using Broker.Model;
using Broker.Service;
using Broker.Static;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Broker
{
    class Redis
    {
        private readonly IBrokerService _service;

        public Redis(IBrokerService service)
        {
            _service = service;
        }

        public void Work()
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

                    if (values.Count > 10)
                    {
                        var jsonValues = DeserializeValues(values);

                        _service.AddAddresses(jsonValues);

                        _service.SaveValues(jsonValues);

                        values.Clear();
                    }
                }

                Thread.Sleep(1500);
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
