using Database.Entities;
using Device.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceRepository : IAppRepository
    {
        Task<GetDeviceDto> GetDevice(int id);
        Task<IEnumerable<GetDeviceDto>> GetDevices(short? deviceTypeId = null, short? deviceKindId = null);

        Task<bool> ExistsDeviceType(short deviceTypeId);
        Task<bool> ExistsDeviceKind(short deviceKindId);
        Task<Database.Entities.Device> GetDeviceByMacAddress(string macAddress);

        Task<DeviceType> GetDeviceType(string type);
        Task<IEnumerable<DeviceType>> GetDeviceTypes();


        Task<DeviceKind> GetDeviceKind(string kind);
        Task<IEnumerable<DeviceKind>> GetDeviceKinds();
    }
}