using Database.Enums;
using System;

namespace Database.Entities
{
    public class DeviceType
    {
        public short Id { get; set; }
        public DateTime Created { get; set; }
        public string Type { get; set; }
        public DeviceRoleEnum Role { get; set; }
    }
}