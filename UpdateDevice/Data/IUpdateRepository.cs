using Database.Entities;
using System.Threading.Tasks;

namespace UpdateDevice.Data
{
    public interface IUpdateRepository
    {
        Task<Device> GetDevice(string macAddress);
        Task<Version> GetLatestDeviceTypeVersion(short deviceTypeId, short deviceKindId);
        Task<Version> GetVersionById(int id);
        Task<DeviceVersion> GetDeviceVersionByDeviceId(int deviceId);
        Task<bool> AddDevice(Device device);
        Task<bool> ConfirmUpdateDevice(Device device, Version version);
    }
}