using Device.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceRepository : IAppRepository
    {
        Task<DeviceDto> GetDevice(int id);
        Task<IEnumerable<DeviceDto>> GetDevices();
    }
}