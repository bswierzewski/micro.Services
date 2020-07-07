using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class AddDeviceDto
    {

        [Required]
        public string MacAddress { get; set; }

        public short? DeviceTypeId { get; set; }
        public short? DeviceKindId { get; set; }
        public int? VersionId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
    }
}
