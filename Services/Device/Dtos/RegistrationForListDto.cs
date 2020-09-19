using Database.Entities;
using System;

namespace Device.Dtos
{
    public class RegistrationForListDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public Address MacAddress { get; set; }
        public Address BleAddress { get; set; }
        public int Rssi { get; set; }
    }
}
