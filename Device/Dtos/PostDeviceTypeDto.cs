using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class AddDeviceTypeDto
    {
        [Required]
        public string Type { get; set; }
    }
}
