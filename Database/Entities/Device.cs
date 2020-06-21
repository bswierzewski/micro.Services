using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActivated { get; set; }
        public string MacAddress { get; set; }
        public string Name { get; set; }
        public bool IsArchival { get; set; }
        public string PhotoUrl { get; set; }

        public int? VersionId { get; set; }
        public Version Version { get; set; }

        public short DeviceTypeId { get; set; }
        [ForeignKey("DeviceTypeId")]
        public DeviceKind DeviceType { get; set; }
    }
}