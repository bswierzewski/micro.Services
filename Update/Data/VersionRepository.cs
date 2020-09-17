using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<bool> AddVersion(Database.Entities.Version version)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UploadFile(FileData fileDatas)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Database.Entities.Version>> GetVersions()
        {
            return await GetVersionsQueryable().ToListAsync();
        }

        public async Task<Database.Entities.Version> GetVersion(int id)
        {
            return await GetVersionsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        private IQueryable<Database.Entities.Version> GetVersionsQueryable()
        {
            return _context.Versions.Include(x => x.DeviceComponent).Include(x => x.Kind).Include(x => x.FileData).AsQueryable();
        }
    }
}
