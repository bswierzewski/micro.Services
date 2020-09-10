using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities.DeviceInfo
{
    /// <summary>
    /// Components 
    /// Tracker
    /// Scanner
    /// Locator
    /// etc.
    /// </summary>
    public class DeviceComponent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string IconName { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
