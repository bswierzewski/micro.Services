using RedisBrokerService.Static;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RedisBrokerService
{
    class Program
    {
        static void Main(string[] args)
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

                    if (values.Count > 9)
                    {
                        Console.WriteLine($"Save to database {values.Count}");

                        values.Clear();
                    }
                }

                Thread.Sleep(500);
            }
        }
    }
}
