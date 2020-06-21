using Device.Enums;
using System;

namespace Device.Models
{
    public class DeviceKind
    {
        public short Id { get; set; }
        public DateTime Created { get; set; }
        public string Kind { get; set; }
        public DeviceRoleEnum Role { get; set; }
    }
}