using Database.Entities;
using System.Threading.Tasks;

namespace UpdateDevice.Data
{
    public interface IUpdateRepository
    {
        Task<bool> IsVersionExists(short major, short minor, short patch);
        Task<bool> IsVersionExists(int versionId);

        Task<Version> GetLatestVersion();
        Task<Version> GetVersionById(int id);
        Task<Version> GetVersionByDeviceId(int versionId);
        Task<Device> GetDevice(string macAddress);
        Task<Device[]> GetDevicesByVersionId(int versionId);
        Task<FileData> GetFileDataById(int fileDataId);

        Task<DeviceVersion> GetDeviceVersionByDeviceId(int deviceId);

        Task<DeviceType> GetDeviceType(string deviceType);

        Task<bool> AddDevice(Device device);
        Task<bool> AddVersion(Version version);
        Task<bool> AddDeviceVersion(DeviceVersion deviceVersion);
        Task<bool> UploadFile(FileData fileDatas);
        Task<FileData> DownloadFile(int fileId);
        Task<bool> ConfirmUpdateDevice(Device device, Version version);
    }
}