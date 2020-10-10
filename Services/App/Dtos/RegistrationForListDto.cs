using Database.Entities;
using System;

namespace Device.Dtos
{
    public class RegistrationForListDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string MacAddress { get; set; }
        public string BleAddress { get; set; }
        public int Rssi { get; set; }
    }
}
