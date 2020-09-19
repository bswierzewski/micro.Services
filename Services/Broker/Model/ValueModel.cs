using System;
using System.Collections.Generic;
using System.Text;

namespace Broker.Model
{
    public class ValueModel
    {
        public DateTime Time { get; set; }
        public string MacAddress { get; set; }
        public string BleAddress { get; set; }
        public int Rssi { get; set; }
    }
}
