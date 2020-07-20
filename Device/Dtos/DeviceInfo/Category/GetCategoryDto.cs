using System;

namespace Device.Data.DeviceInfo.Category
{
    public class GetCategoryDto
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public DateTime Created { get; internal set; }
    }
}