using Database.Entities;
using System.Threading.Tasks;

namespace Update.Data
{
    public interface IUpdateRepository
    {
        Task<T> Find<T>(int primaryKey) where T : class;
        Task<bool> Add<T>(T entity) where T : class;
        Task<bool> SaveAllChangesAsync();
        Task<int?> GetAddressId(string address);
        Task<Device> GetDeviceByAddresId(int value);
        Task<Version> GetLatestVersion(int? kindId, int? componentId);

    }
}