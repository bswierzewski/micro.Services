using System;

namespace Device.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DeviceComponentDto[] DeviceComponents { get; set; }

    }
}