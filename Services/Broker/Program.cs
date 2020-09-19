using Broker.Service;
using System;

namespace Broker
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    new Redis(new BrokerService()).Work();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                    continue;
                }
            }
        }
    }
}