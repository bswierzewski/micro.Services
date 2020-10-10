using Device.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceRepository : IAppRepository
    {
        Task<ICollection<Database.Entities.Device>> GetDevices(DeviceParams deviceParams = null);
        Task<Database.Entities.Address> GetAddress(string address);
        Task<Database.Entities.Address> AddAddress(string address);
        Task<Database.Entities.Device> GetDevice(int id);
    }
}