using System.ComponentModel.DataAnnotations;

namespace UpdateDevice.Models
{
    public class DeviceVersion
    {
        [Key]
        public int DeviceId { get; set; }
        public Device Device { get; set; }

        [Key]
        public int VersionId { get; set; }
        public Version Version { get; set; }
    }
}
