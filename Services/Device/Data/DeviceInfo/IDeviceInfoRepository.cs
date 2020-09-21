using Database.Entities;
using Device.Dtos;
using Device.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data.DeviceInfo
{
    public interface IDeviceInfoRepository : IAppRepository
    {
        Task<Category> GetCategory(int id);
        Task<IEnumerable<Category>> GetCategories();

        Task<IEnumerable<Component>> GetComponents(ComponentParams componentParams = null);
        Task<ICollection<Component>> GetComponentsByIds(IEnumerable<int> componentIds);
        Task<bool> UpdateComponents(int id, CategoryDto categoryDto);

        Task<IEnumerable<Kind>> GetKinds();
    }
}
