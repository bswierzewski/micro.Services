using Device.Data.DeviceInfo.Category;
using Device.Data.DeviceInfo.Component;
using Device.Dtos.DeviceInfo;
using Device.Dtos.DeviceInfo.Kind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Data.DeviceInfo
{
    public interface IDeviceInfoRepository : IAppRepository
    {
        Task<IEnumerable<GetKindDto>> GetKinds(int? kindId = null);
        Task<IEnumerable<GetComponentDto>> GetComponents(int? componentId = null);
        Task<IEnumerable<GetCategoryDto>> GetCategories(int? categoryId = null);
    }
}
