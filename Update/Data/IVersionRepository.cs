using Database.Entities;
using System.Threading.Tasks;
using Update.Dtos;

namespace Update.Data
{
    public interface IVersionRepository
    {
        Task<VersionInfoDto[]> GetAllVersion();
        Task<bool> IsDeviceTypeExists(short deviceTypeId);
        Task<bool> IsVersionExists(short major, short minor, short patch, short deviceTypeId, short deviceKindId);
        Task<bool> UploadFile(FileData fileDatas);
        Task<Device> GetDevice(string macAddress);
        Task<Device> GetDevice(int devicceId);
        Task<Version> GetVersionById(int id);
        Task<DeviceVersion> GetDeviceVersion(int deviceId);
        Task<bool> AddDeviceVersion(DeviceVersion deviceVersion);
        Task<bool> AddVersion(Version newVersion);
        Task<bool> IsDeviceKindExists(short deviceKindId);
        Task<bool> SaveAllChanges();
    }
}
