using Database.Entities;
using System.Threading.Tasks;

namespace Update.Data
{
    public interface IUpdateRepository : IAppRepository
    {
        Task<int?> GetAddressId(string address);
        Task<Device> GetDeviceByAddresId(int value);
        Task<int?> GetLatestVersionId(int? kindId, int? componentId);
        Task<Version> GetVersionById(int versionId);
    }
}