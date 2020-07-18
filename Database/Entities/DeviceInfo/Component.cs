using Database.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities.DeviceInfo
{
    public class Component
    {
        public short TypeId { get; set; } = (short)TypeEnum.Component;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }

        public short? CategoryTypeId { get; set; } = (short)TypeEnum.Category;
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
