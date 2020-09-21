using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
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
        public string Icon { get; set; }
        public virtual ICollection<Component> Components { get; set; }
    }
}
