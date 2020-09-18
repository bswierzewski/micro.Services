using Device.Dtos;
using Device.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceRepository : IAppRepository
    {
        Task<ICollection<Database.Entities.Device>> GetDevices(DeviceParams deviceParams = null);
        Task<Database.Entities.Device> GetDevice(int deviceId);
        Task<bool> ExistsDevice(int addressId);
    }
}