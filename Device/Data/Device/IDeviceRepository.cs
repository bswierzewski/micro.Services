using Device.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceRepository : IAppRepository
    {
        Task<GetDeviceDto> GetDeviceDtoById(int id);
        Task<IEnumerable<GetDeviceDto>> GetDevicesDto();
        Task<IEnumerable<GetDeviceDto>> GetDevicesDtoByType(short deviceTypeId);
        Task<IEnumerable<GetDeviceDto>> GetDevicesDtoByKind(short deviceKindId);

        Task<bool> IsDevice(string macAddress);
        Task<Database.Entities.Device> GetDevice(int deviceId);
    }
}