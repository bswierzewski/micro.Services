using Device.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceRepository : IAppRepository
    {
        Task<ICollection<Database.Entities.Device>> GetDevices(DeviceParams deviceParams = null);
        Task<int?> GetAddressId(string address);
        Task<int?> AddAddress(string address);
    }
}