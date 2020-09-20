using System;
using System.Collections.Generic;

namespace Device.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public IEnumerable<int> DeviceComponentIds { get; set; }

    }
}