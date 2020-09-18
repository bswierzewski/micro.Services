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

        public async Task<Version> GetVersionById(int id)
        {
            return await _context.Versions.Include(x => x.FileData).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Device> GetDevice(int? macAddressId)
        {
            return await _context.Devices
                .FirstOrDefaultAsync(x => x.AddressId == macAddressId);
        }

        public async Task<bool> ConfirmUpdateDevice(Device device, Version version)
        {
            var deviceResult = await _context.Devices.FirstOrDefaultAsync(x => x.Id == device.Id);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int?> GetAddressId(string macAddress)
        {
            return await _context.Addresses.Where(x => x.Label == macAddress).Select(x => x.Id).FirstOrDefaultAsync();
        }
    }
}