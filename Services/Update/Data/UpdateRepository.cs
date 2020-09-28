using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Update.Data
{
    public class UpdateRepository : IUpdateRepository
    {
        private readonly DataContext _context;

        public UpdateRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);

            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<T> Find<T>(int primaryKey) where T : class
        {
            return await _context.FindAsync<T>(primaryKey);
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int?> GetAddressId(string address)
        {
            return await _context.Addresses.Where(x => x.Label == address).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<Device> GetDeviceByAddresId(int addressId)
        {
            return await _context.Devices.Where(x => x.AddressId == addressId).FirstOrDefaultAsync();
        }

        public async Task<Version> GetVersionById(int id)
        {
            return await _context.Versions.Include(x => x.FileData).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Version> GetLatestVersion(int? kindId, int? componentId)
        {
            return await _context.Versions.Where(x => x.KindId == kindId && x.ComponentId == componentId)
                .OrderByDescending(x => x.Major)
                .ThenByDescending(x => x.Minor)
                .ThenByDescending(x => x.Patch)
                .FirstOrDefaultAsync();
        }
    }
}