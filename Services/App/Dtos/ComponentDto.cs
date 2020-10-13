using System;
using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class ComponentDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        public int? CategoryId { get; set; }
    }
}