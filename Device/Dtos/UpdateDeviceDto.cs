using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class UpdateDeviceDto
    {
        [Required]
        public int DeviceId { get; set; }

        [Required]
        public short Payload { get; set; }
    }
}
