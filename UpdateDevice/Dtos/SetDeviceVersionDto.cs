using System.ComponentModel.DataAnnotations;

namespace UpdateDevice.Dtos
{
    public class SetDeviceVersionDto
    {
        [Required]
        public int DeviceId { get; set; }

        [Required]
        public int VersionId { get; set; }
    }
}
