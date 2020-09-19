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
                    values.Add(value);

                    if (values.Count > 9)
                    {
                        var jsonValues = DeserializeValues(values);

                        var addresses = GetAddresses(jsonValues);

                        _service.SaveAddresses(addresses);

                        _service.SaveValues(jsonValues);

                        values.Clear();
                    }
                }

                Thread.Sleep(1500);
            }
        }

        private HashSet<string> GetAddresses(List<ValueModel> values)
        {
            HashSet<string> results = new HashSet<string>();

            values.ForEach(json =>
            {
                results.Add(json.MacAddress);

                results.Add(json.BleAddress);
            });

            return results;
        }

        private List<ValueModel> DeserializeValues(List<RedisValue> redisValue)
        {
            var resultValues = new List<ValueModel>();

            redisValue.ForEach(value =>
            {
                resultValues.Add(JsonConvert.DeserializeObject<ValueModel>(value));
            });

            return resultValues;
        }
    }
}
