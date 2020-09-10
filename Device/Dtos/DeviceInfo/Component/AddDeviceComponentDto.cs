using System.ComponentModel.DataAnnotations;

namespace Device.Data.DeviceInfo.DeviceComponent
{
    public class AddDeviceComponentDto
    {
        [Required]
        public string Name { get; set; }
        public int? CategoryId { get; set; }
    }
}