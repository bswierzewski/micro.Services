using System;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class FileData
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public byte[] Content { get; set; }
    }
}
