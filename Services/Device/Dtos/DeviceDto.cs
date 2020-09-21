using System;
using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class DeviceDto
    {
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        public bool? IsAutoUpdate { get; set; }
        public int? CategoryId { get; set; }
        public int? KindId { get; set; }
        public int? ComponentId { get; set; }
        public int? ActuallVersionId { get; set; }
        public int? SpecificVersionId { get; set; }
    }
}
