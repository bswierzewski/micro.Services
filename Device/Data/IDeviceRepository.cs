using Database.Entities;
using Device.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceRepository : IAppRepository
    {
        Task<GetDeviceDto> GetDevice(int id);
        Task<IEnumerable<GetDeviceDto>> GetDevices(int? deviceTypeId = null);
        Task<DeviceType> GetDeviceType(string type);
        Task<IEnumerable<DeviceType>> GetDeviceTypes();
        Task<bool> ExistsDevice(string macAddress);
        Task<bool> ExistsDeviceType(short deviceTypeId);
        Task<DeviceType> GetDeviceTypeById(short typeId);
    }
}