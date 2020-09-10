using Device.Data.DeviceInfo.DeviceComponent;
using System;

namespace Device.Data.DeviceInfo.Category
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public GetDeviceComponentDto[] DeviceComponents { get; set; }

    }
}