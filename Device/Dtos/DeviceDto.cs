using System;

namespace Device.Dtos
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActivated { get; set; }
        public string MacAddress { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchival { get; set; }
        public string PhotoUrl { get; set; }
    }
}