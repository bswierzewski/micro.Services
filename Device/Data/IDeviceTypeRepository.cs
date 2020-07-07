using Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceTypeRepository
    {
        Task<bool> IsDeviceType(short deviceTypeId);

        Task<DeviceType> GetDeviceType(string type);
        Task<DeviceType> GetDeviceType(int typeId);
        Task<IEnumerable<DeviceType>> GetDeviceTypes();

    }
}