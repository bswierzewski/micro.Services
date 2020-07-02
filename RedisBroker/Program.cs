using RedisBroker.Static;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RedisBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisChannel key = "micro";
            RedisChannel key2 = "micro";
            RedisValue value;

            Random rnd = new Random();

            ConnectionMultiplexer redisConn = ConnectionMultiplexer.Connect(Urls.ConnectionRedis);

            IDatabase db = redisConn.GetDatabase();

            while (true)
            {
                var rand = rnd.Next(1, 4);

                key2 = rand.ToString();

                value = $"{{\"time\": \"{DateTime.Now}\",\"name\": \"{rand}\",\"address\": \"{rnd.Next(100, 1000)}\",\"rssi\":\"{rnd.Next(50, 100)}\" }}";

                db.Publish(key, value);
                db.Publish(key2, value);

                Thread.Sleep(2000);
            }
        }
    }
    class Redis
    {
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