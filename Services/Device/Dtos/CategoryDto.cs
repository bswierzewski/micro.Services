using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        public IEnumerable<int> DeviceComponentIds { get; set; }

    }
}