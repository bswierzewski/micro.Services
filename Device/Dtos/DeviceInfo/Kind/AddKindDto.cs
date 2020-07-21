using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Dtos.DeviceInfo.Kind
{
    public class AddKindDto
    {
        [Required]
        public string Name { get; set; }
    }
}
