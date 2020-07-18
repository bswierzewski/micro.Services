using Database.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Version
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public short Major { get; set; }
        public short Minor { get; set; }
        public short Patch { get; set; }

        public short? ComponentTypeId { get; set; } = (short)TypeEnum.Component;
        public int? ComponentId { get; set; }

        public short? KindTypeId { get; set; } = (short)TypeEnum.Kind;
        public int? KindId { get; set; }

        public int FileDataId { get; set; }
        public FileData FileData { get; set; }
    }
}