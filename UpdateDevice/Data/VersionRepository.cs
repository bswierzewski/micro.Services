using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpdateDevice.Dtos;

namespace UpdateDevice.Data
{
    public class VersionRepository : IVersionRepository
    {
        private readonly DataContext _context;
        public VersionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAllChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddDeviceVersion(DeviceVersion deviceVersion)
        {
            await _context.DeviceVersions.AddAsync(deviceVersion);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<DeviceVersion> GetDeviceVersion(int deviceId)
        {
            return await _context.DeviceVersions.FirstOrDefaultAsync(x => x.DeviceId == deviceId);
        }

        public async Task<bool> AddVersion(Version version)
        {
            await _context.Versions.AddAsync(version);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<VersionInfoDto[]> GetAllVersion()
        {
            return await (from version in _context.Versions
                          join file in _context.FileDatas on version.FileDataId equals file.Id
                          join types in _context.DeviceTypes on version.DeviceTypeId equals types.Id
                          join kinds in _context.DeviceKinds on version.DeviceTypeId equals kinds.Id
                          select new VersionInfoDto
                          {
                              FileCreated = file.Created,
                              DeviceType = types.Type,
                              DeviceKind = kinds.Kind,
                              FileDataId = file.Id,
                              FileName = file.Name,
                              Major = version.Major,
                              Minor = version.Minor,
                              Patch = version.Patch,
                              VersionCreated = version.Created,
                              VersionId = version.Id
                          })
                .ToArrayAsync();
        }

        public async Task<Device> GetDevice(string macAddress)
        {
            return await _context.Devices.FirstOrDefaultAsync(x => x.MacAddress == macAddress);
        }

        public async Task<Device> GetDevice(int deviceId)
        {
            return await _context.Devices.FirstOrDefaultAsync(x => x.Id == deviceId);
        }

        public async Task<Version> GetVersionById(int id)
        {
            return await _context.Versions.Include(x => x.FileData).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsDeviceTypeExists(short deviceTypeId)
        {
            return await _context.DeviceTypes.AnyAsync(x => x.Id == deviceTypeId);
        }
        public async Task<bool> IsDeviceKindExists(short deviceKindId)
        {
            return await _context.DeviceKinds.AnyAsync(x => x.Id == deviceKindId);
        }

        public async Task<bool> IsVersionExists(short major, short minor, short patch, short deviceTypeId, short deviceKindId)
        {
            return await _context.Versions.AnyAsync(x => x.Major == major && x.Minor == minor && x.Patch == patch && x.DeviceTypeId == deviceTypeId && x.DeviceKindId == deviceKindId);
        }

        public async Task<bool> UploadFile(FileData fileDatas)
        {
            await _context.FileDatas.AddAsync(fileDatas);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
