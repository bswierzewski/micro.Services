using System;

namespace Scanner.Model
{
    class RedisValueModel
    {
        public string time { get; set; }
        public string macAddress { get; set; }
        public string bleAddress { get; set; }
        public int rssi { get; set; }
    }
}
