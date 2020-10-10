using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class KindDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }
    }
}
