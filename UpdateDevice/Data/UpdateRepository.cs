using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UpdateDeviceService.Models;

namespace UpdateDeviceService.Data
{
    public class UpdateRepository : IUpdateRepository
    {
        private readonly DataContext _context;
        public UpdateRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> IsVersionExists(int versionId)
        {
            return await _context.Versions.AnyAsync(x => x.Id == versionId);
        }

        public async Task<bool> IsVersionExists(short major, short minor, short patch)
        {
            return await _context.Versions.AnyAsync(x => x.Major == major && x.Minor == minor && x.Patch == patch);
        }

        public async Task<Version> GetLatestVersion()
        {
            return await _context.Versions.OrderByDescending(x => x.Major).ThenByDescending(x => x.Minor).ThenByDescending(x => x.Patch).FirstOrDefaultAsync();
        }

        public async Task<Version> GetVersionById(int id)
        {
            return await _context.Versions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Version> GetVersionByDeviceId(int deviceId)
        {
            var deviceVersion = await _context.DeviceVersions.FirstOrDefaultAsync(x => x.DeviceId == deviceId);

            return await _context.Versions.FirstOrDefaultAsync(x => x.Id == deviceVersion.VersionId);
        }

        public async Task<Device> GetDevice(string macAddress)
        {
            return await _context.Devices.FirstOrDefaultAsync(x => x.MacAddress == macAddress);
        }

        public async Task<Device[]> GetDevicesByVersionId(int versionId)
        {
            var deviceVersion = await _context.DeviceVersions.FirstOrDefaultAsync(x => x.VersionId == versionId);

            return await _context.Devices.Where(x => x.VersionId == deviceVersion.VersionId).ToArrayAsync();
        }

        public async Task<FileData> GetFileDataById(int fileDataId)
        {
            return await _context.FileDatas.FirstOrDefaultAsync(x => x.Id == fileDataId);
        }

        public async Task<DeviceVersion> GetDeviceVersionByDeviceId(int deviceId)
        {
            return await _context.DeviceVersions.FirstOrDefaultAsync(x => x.DeviceId == deviceId);
        }

        public async Task<bool> AddDevice(Device device)
        {
            await _context.Devices.AddAsync(device);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddVersion(Version version)
        {
            await _context.Versions.AddAsync(version);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddDeviceVersion(DeviceVersion deviceVersion)
        {
            await _context.DeviceVersions.AddAsync(deviceVersion);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UploadFile(FileData fileDatas)
        {
            await _context.FileDatas.AddAsync(fileDatas);

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<FileData> DownloadFile(int fileId)
        {
            return await _context.FileDatas.FirstOrDefaultAsync(x => x.Id == fileId);
        }
        public async Task<bool> ConfirmUpdateDevice(Device device, Version version)
        {
            var deviceResult = await _context.Devices.FirstOrDefaultAsync(x => x.Id == device.Id);

            deviceResult.VersionId = version.Id;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}