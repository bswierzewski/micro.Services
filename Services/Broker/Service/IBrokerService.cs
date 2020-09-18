using Broker.Model;
using StackExchange.Redis;
using System.Collections.Generic;

namespace Broker.Service
{
    public interface IBrokerService
    {
        void SaveValues(List<RedisValueModel> jsonValues);
        void AddToTempDevices(List<RedisValueModel> jsonValues);
    }
}