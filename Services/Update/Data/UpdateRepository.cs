using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Update.Data
{
    public class UpdateRepository : AppRepository, IUpdateRepository
    {
        public UpdateRepository(DataContext context) : base(context) { }

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