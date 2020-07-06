using Database.Entities;
using System.Threading.Tasks;
using UpdateDevice.Dtos;

namespace UpdateDevice.Data
{
    public interface IVersionRepository
    {
        Task<VersionInfoDto[]> GetAllVersion();
        Task<bool> IsVersionExists(short major, short minor, short patch);
        Task<bool> UploadFile(FileData fileDatas);
        Task<Device> GetDevice(string macAddress);
        Task<FileData> GetFileDataById(int fileDataId);
        Task<Version> GetVersionById(int id);
        Task<bool> AddDeviceVersion(DeviceVersion deviceVersion);
        Task<bool> AddVersion(Version newVersion);
    }
}
