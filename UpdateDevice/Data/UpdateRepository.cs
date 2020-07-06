using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace UpdateDevice.Data
{
    public class UpdateRepository : IUpdateRepository
    {
        private readonly DataContext _context;
        public UpdateRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Version> GetLatestDeviceTypeVersion(int deviceTypeId)
        {
            return await _context.Versions.Where(x => x.DeviceTypeId == deviceTypeId).OrderByDescending(x => x.Major).ThenByDescending(x => x.Minor).ThenByDescending(x => x.Patch).FirstOrDefaultAsync();
        }

        public async Task<Version> GetVersionById(int id)
        {
            return await _context.Versions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Device> GetDevice(string macAddress)
        {
            return await _context.Devices.FirstOrDefaultAsync(x => x.MacAddress == macAddress);
        }

        public async Task<DeviceVersion> GetDeviceVersionByDeviceId(int deviceId)
        {
            return await _context.DeviceVersions.FirstOrDefaultAsync(x => x.DeviceId == deviceId);
        }

        public async Task<DeviceType> GetDeviceType(string deviceType)
        {
            return await _context.DeviceTypes.FirstOrDefaultAsync(x => x.Name == deviceType.ToLower());
        }

        public async Task<bool> AddDevice(Device device)
        {
            await _context.Devices.AddAsync(device);

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