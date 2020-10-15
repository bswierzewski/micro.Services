using System;

namespace Update.Dtos
{
    public class VersionForListDto
    {
        public int Id { get; set; }
        public string Created { get; set; }
        public string Name { get; set; }
        public short Major { get; set; }
        public short Minor { get; set; }
        public short Patch { get; set; }
        public string Component { get; set; }
        public string Kind { get; set; }
        public string FileData { get; set; }
    }
}
