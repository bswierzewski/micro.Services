using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Dtos.DeviceInfo.Category
{
    public class AddCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
