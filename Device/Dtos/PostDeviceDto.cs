using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class PostDeviceDto
    {
        [Required]
        public short DeviceTypeId { get; set; }
        [Required]
        public string MacAddress { get; set; }

        public string Name { get; set; }
    }
}
