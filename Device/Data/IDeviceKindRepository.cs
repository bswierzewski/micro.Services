using Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceKindRepository
    {
        Task<bool> IsDeviceKind(short deviceKindId);

        Task<DeviceKind> GetDeviceKind(string kind);
        Task<DeviceKind> GetDeviceKind(int kindId);
        Task<IEnumerable<DeviceKind>> GetDeviceKinds();
    }
}