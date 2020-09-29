using Database.Entities;
using System.Threading.Tasks;

namespace Update.Data
{
    public interface IUpdateRepository : IAppRepository
    {
        Task<int?> GetAddressId(string address);
        Task<Device> GetDeviceByAddresId(int value);
        Task<Version> GetLatestVersion(int? kindId, int? componentId);

    }
}