using Database.Entities;
using System.Threading.Tasks;

namespace Update.Data
{
    public interface IUpdateRepository
    {
        Task<Device> GetDevice(string macAddress);
        Task<Version> GetLatestDeviceTypeVersion(short deviceTypeId, short deviceKindId);
        Task<Version> GetVersionById(int id);
        Task<Version> GetDeviceVersionByDeviceId(int deviceId);
        Task<bool> AddDevice(Device device);
        Task<bool> ConfirmUpdateDevice(Device device, Version version);
    }
}