using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class AddDeviceDto
    {
        [Required]
        public string MacAddress { get; set; }
    }
}
