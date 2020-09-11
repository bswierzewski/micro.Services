using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities.DeviceInfo
{
    /// <summary>
    /// Category of devices
    /// Light
    /// Sensor
    /// Misc
    /// </summary>
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string IconName { get; set; }
        public virtual ICollection<DeviceComponent> DeviceComponents { get; set; }
    }
}
