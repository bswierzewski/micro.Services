using System.ComponentModel.DataAnnotations;

namespace Update.Dtos
{
    public class SetDeviceVersionDto
    {
        [Required]
        public int DeviceId { get; set; }

        [Required]
        public int VersionId { get; set; }
    }
}
