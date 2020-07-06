using System;

namespace Device.Dtos
{
    public class GetDeviceDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string MacAddress { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string Type { get; set; }
        public string PhotoUrl { get; set; }
    }
}