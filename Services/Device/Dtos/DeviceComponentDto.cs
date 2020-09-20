using System;

namespace Device.Dtos
{
    public class DeviceComponentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int? CategoryId { get; set; }
    }
}