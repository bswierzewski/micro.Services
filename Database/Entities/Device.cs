using System;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string MacAddress { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }

        public short DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }
        public int? VersionId { get; set; }
        public Version Version { get; set; }

    }
}