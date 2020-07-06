using Database.Entities;
using System.Threading.Tasks;
using UpdateDevice.Dtos;

namespace UpdateDevice.Data
{
    public interface IVersionRepository
    {
        Task<VersionInfoDto[]> GetAllVersion();
        Task<bool> IsDeviceTypeExists(short deviceTypeId);
        Task<bool> IsVersionExists(short major, short minor, short patch, short deviceTypeId);
        Task<bool> UploadFile(FileData fileDatas);
        Task<Device> GetDevice(string macAddress);
        Task<Version> GetVersionById(int id);
        Task<bool> AddDeviceVersion(DeviceVersion deviceVersion);
        Task<bool> AddVersion(Version newVersion);
    }
}
