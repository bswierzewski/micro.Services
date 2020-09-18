using System;
using System.Collections.Generic;
using System.Text;

namespace Broker.Model
{
    public class RedisValueModel
    {
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public string MacAddress { get; set; }
        public int Rssi { get; set; }
    }
}
