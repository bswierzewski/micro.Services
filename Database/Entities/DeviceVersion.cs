using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class DeviceVersion
    {
        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public int VersionId { get; set; }
        public Version Version { get; set; }
    }
}
