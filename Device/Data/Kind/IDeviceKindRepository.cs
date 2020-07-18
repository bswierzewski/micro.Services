using Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IDeviceKindRepository : IAppRepository
    {
        Task<bool> IsDeviceKind(string kind);

        Task<Kind> GetDeviceKind(string kind);
        Task<Kind> GetDeviceKind(int kindId);
        Task<IEnumerable<Kind>> GetDeviceKinds();
    }
}