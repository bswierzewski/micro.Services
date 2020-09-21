using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        public IEnumerable<int> Components { get; set; }

    }
}