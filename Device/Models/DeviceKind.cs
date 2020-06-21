using DeviceService.Enums;
using System;

namespace DeviceService.Models
{
    public class DeviceKind
    {
        public short Id { get; set; }
        public DateTime Created { get; set; }
        public string Kind { get; set; }
        public DeviceRoleEnum Role { get; set; }
    }
}