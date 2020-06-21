using System.Collections.Generic;
using System.Threading.Tasks;
using DeviceService.Dtos;

namespace DeviceService.Data
{
    public interface IDeviceRepository : IAppRepository
    {
        Task<DeviceDto> GetDevice(int id);
        Task<DeviceDto> GetDevice(int id, Enums.DeviceRoleEnum typeEnum);
        Task<IEnumerable<DeviceDto>> GetDevices();
        Task<IEnumerable<DeviceDto>> GetDevices(Enums.DeviceRoleEnum typeEnum);
    }
}