using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class PostDeviceTypeDto
    {
        [Required]
        public string Type { get; set; }
    }
}
