using System;
using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class ComponentDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        public int? CategoryId { get; set; }
    }
}