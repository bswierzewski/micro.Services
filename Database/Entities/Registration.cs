using System;

namespace Database.Entities
{
    public class Registration
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int Rssi { get; set; }
        public string ScannerMacAddress { get; set; }
        public string LocatorMacAddress { get; set; }

    }
}