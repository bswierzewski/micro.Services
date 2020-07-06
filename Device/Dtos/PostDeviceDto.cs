using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class PostDeviceDto
    {

        [Required]
        public string MacAddress { get; set; }

        [Required]
        public short? DeviceTypeId { get; set; }

        [Required]
        public short? DeviceKindId { get; set; }
        public int? VersionId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
    }
}
