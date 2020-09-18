using Broker.Service;

namespace Broker
{
    class Program
    {
        static void Main(string[] args)
        {
            new Redis(new BrokerService()).Work();
        }
    }
}