using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class AddDeviceDto
    {
        [Required]
        public string MacAddress { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public int? KindId { get; set; }
        public int? DeviceComponentId { get; set; }
        public int? ActuallVersionId { get; set; }
        public int? SpecificVersionId { get; set; }
    }
}
