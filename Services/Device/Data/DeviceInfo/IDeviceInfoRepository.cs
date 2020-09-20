using Database.Entities.DeviceInfo;
using Device.Dtos;
using Device.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data.DeviceInfo
{
    public interface IDeviceInfoRepository : IAppRepository
    {
        Task<IEnumerable<Kind>> GetKinds(int? kindId = null);
        Task<DeviceComponent> GetDeviceComponent(int deviceComponentId);
        Task<IEnumerable<DeviceComponent>> GetDeviceComponents(DeviceComponentParams deviceComponentParams = null);
        Task<IEnumerable<Category>> GetCategories(int? categoryId = null);
        Task<ICollection<DeviceComponent>> GetDeviceComponentsByIds(IEnumerable<int> deviceComponentIds);
        Task<bool> UpdateDeviceComponents(CategoryDto categoryDto);
    }
}
