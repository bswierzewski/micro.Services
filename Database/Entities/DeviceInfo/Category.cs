using Database.Enums;
using System;
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
        public short TypeId { get; set; } = (short)TypeEnum.Category;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
    }
}
