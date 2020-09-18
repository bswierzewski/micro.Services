using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities.DeviceInfo
{
    /// <summary>
    /// Kind of devices 
    /// ESP32
    /// ESP8266
    /// etc.
    /// </summary>
    public class Kind
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string PhotoUrl { get; set; }
    }
}
