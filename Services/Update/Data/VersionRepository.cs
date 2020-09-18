using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            await _context.Versions.AddAsync(version);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UploadFile(FileData fileDatas)
        {
            await _context.FileDatas.AddAsync(fileDatas);

            return await _context.SaveChangesAsync() > 0;
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
