using Database.Entities;
using System.Threading.Tasks;

namespace UpdateDevice.Data
{
    public interface IUpdateRepository
    {
        Task<Device> GetDevice(string macAddress);
        Task<Version> GetLatestDeviceTypeVersion(int deviceTypeId);
        Task<Version> GetVersionById(int id);
        Task<DeviceVersion> GetDeviceVersionByDeviceId(int deviceId);
        Task<DeviceType> GetDeviceType(string deviceType);
        Task<bool> AddDevice(Device device);
        Task<FileData> DownloadFile(int fileId);
        Task<bool> ConfirmUpdateDevice(Device device, Version version);
    }
}