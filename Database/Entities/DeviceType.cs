using System;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class DeviceType
    {
        [Key]
        public short Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}