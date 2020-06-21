using Database.Enums;
using Device.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceRepository : IAppRepository
    {
        Task<DeviceDto> GetDevice(int id);
        Task<DeviceDto> GetDevice(int id, DeviceRoleEnum typeEnum);
        Task<IEnumerable<DeviceDto>> GetDevices();
        Task<IEnumerable<DeviceDto>> GetDevices(DeviceRoleEnum typeEnum);
    }
}