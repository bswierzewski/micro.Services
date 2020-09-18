using Database.Entities;
using System.Threading.Tasks;

namespace Update.Data
{
    public interface IUpdateRepository
    {
        Task<Device> GetDevice(int? macAddressId);
        Task<Version> GetVersionById(int id);
        Task<bool> ConfirmUpdateDevice(Device device, Version version);
        Task<int?> GetAddressId(string macAddress);
    }
}