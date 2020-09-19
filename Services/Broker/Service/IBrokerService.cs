using Broker.Model;
using System.Collections.Generic;

namespace Broker.Service
{
    public interface IBrokerService
    {
        void SaveAddresses(HashSet<string> addresses);
        void SaveValues(List<ValueModel> jsonValues);
    }
}