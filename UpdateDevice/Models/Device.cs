using System.ComponentModel.DataAnnotations;

namespace UpdateDeviceService.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string MacAddress { get; set; }

        public int? VersionId { get; set; }
        public Version Version { get; set; }
    }
}