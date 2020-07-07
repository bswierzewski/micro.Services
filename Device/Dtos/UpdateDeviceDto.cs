using System.ComponentModel.DataAnnotations;

namespace Device.Dtos
{
    public class UpdateDeviceDto
    {
        [Required]
        public int DeviceId { get; set; }

        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public short? TypeId { get; set; }
        public short? KindId { get; set; }
    }
}
