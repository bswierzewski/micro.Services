using Broker.Model;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Broker.Service
{
    public interface IBrokerService
    {
        Task<bool> SaveValues(List<RedisValueModel> jsonValues);
        Task<bool> AddAddresses(List<RedisValueModel> jsonValues);
    }
}