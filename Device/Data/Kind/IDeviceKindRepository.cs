using Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceKindRepository : IAppRepository
    {
        Task<bool> IsDeviceKind(string kind);

        Task<DeviceKind> GetDeviceKind(string kind);
        Task<DeviceKind> GetDeviceKind(int kindId);
        Task<IEnumerable<DeviceKind>> GetDeviceKinds();
    }
}