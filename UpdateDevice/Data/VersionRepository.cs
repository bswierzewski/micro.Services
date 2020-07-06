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

        public async Task<bool> AddDeviceVersion(DeviceVersion deviceVersion)
        {
            await _context.DeviceVersions.AddAsync(deviceVersion);

            return await _context.SaveChangesAsync() > 0;
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
                          join deviceType in _context.DeviceTypes on version.DeviceTypeId equals deviceType.Id
                          select new VersionInfoDto
                          {
                              FileCreated = file.Created,
                              DeviceType = deviceType.Name,
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

        public async Task<FileData> GetFileDataById(int fileDataId)
        {
            return await _context.FileDatas.FirstOrDefaultAsync(x => x.Id == fileDataId);
        }

        public async Task<Version> GetVersionById(int id)
        {
            return await _context.Versions.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> IsVersionExists(short major, short minor, short patch)
        {
            return await _context.Versions.AnyAsync(x => x.Major == major && x.Minor == minor && x.Patch == patch);
        }

        public async Task<bool> UploadFile(FileData fileDatas)
        {
            await _context.FileDatas.AddAsync(fileDatas);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
