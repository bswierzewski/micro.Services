using Device.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data.DeviceInfo
{
    public interface IDeviceInfoRepository : IAppRepository
    {
        Task<IEnumerable<KindDto>> GetKinds(int? kindId = null);
        Task<IEnumerable<DeviceComponentDto>> GetDeviceComponents(int? deviceComponentId = null);
        Task<IEnumerable<CategoryDto>> GetCategories(int? categoryId = null);
    }
}
