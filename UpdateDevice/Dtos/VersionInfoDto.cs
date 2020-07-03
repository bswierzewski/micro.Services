﻿using System;

namespace UpdateDevice.Dtos
{
    public class VersionInfoDto
    {
        public string FileName { get; set; }
        public DateTime FileCreated { get; set; }
        public int FileDataId { get; set; }
        public int VersionId { get; set; }
        public DateTime VersionCreated { get; set; }
        public short Major { get; set; }
        public short Minor { get; set; }
        public short Patch { get; set; }
    }
}
