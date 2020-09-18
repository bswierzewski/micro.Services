using System;

namespace Scanner.Model
{
    class RedisValueModel
    {
        public string time { get; set; }
        public string scanner { get; set; }
        public string tracker { get; set; }
        public int rssi { get; set; }
    }
}
