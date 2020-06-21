using Database.Enums;
using System;

namespace Database.Entities
{
    public class DeviceKind
    {
        public short Id { get; set; }
        public DateTime Created { get; set; }
        public string Kind { get; set; }
        public DeviceRoleEnum Role { get; set; }
    }
}