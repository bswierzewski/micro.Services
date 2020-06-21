using System;

namespace Database.Entities
{
    public class FileData
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte[] Content { get; set; }
    }
}
