using Database;
using Database.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Update.Dtos;

namespace Update.Data
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

        public async Task<bool> AddDeviceVersion(Database.Entities.Version deviceVersion)
        {
            throw new NotImplementedException();
        }

        public async Task<Database.Entities.Version> GetDeviceVersion(int deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddVersion(Database.Entities.Version version)
        {
            throw new NotImplementedException();
        }

        public async Task<GetVersionDto[]> GetAllVersion()
        {
            throw new NotImplementedException();
        }

        public async Task<Device> GetDevice(string macAddress)
        {
            throw new NotImplementedException();
        }

        public async Task<Device> GetDevice(int deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<Database.Entities.Version> GetVersionById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsDeviceTypeExists(short deviceTypeId)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> IsDeviceKindExists(short deviceKindId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsVersionExists(short major, short minor, short patch, short deviceTypeId, short deviceKindId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UploadFile(FileData fileDatas)
        {
            throw new NotImplementedException();
        }

        public IQueryable<GetVersionDto> GetVersions(Expression<Func<Database.Entities.Version, bool>> expression = null)
        {
            throw new NotImplementedException();
        }
    }
}
