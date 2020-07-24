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

        public async Task<Version> GetLatestDeviceTypeVersion(short deviceTypeId, short deviceKindId)
        {
            return await _context.Versions
                .OrderByDescending(x => x.Major)
                .ThenByDescending(x => x.Minor)
                .ThenByDescending(x => x.Patch)
                .FirstOrDefaultAsync();
        }

        public async Task<Version> GetVersionById(int id)
        {
            return await _context.Versions.Include(x => x.FileData).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Device> GetDevice(string macAddress)
        {
            return await _context.Devices
                .FirstOrDefaultAsync(x => x.MacAddress == macAddress);
        }

        public async Task<Version> GetDeviceVersionByDeviceId(int deviceId)
        {
            return await _context.Versions.FirstOrDefaultAsync();
        }

        public async Task<bool> AddDevice(Device device)
        {
            await _context.Devices.AddAsync(device);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ConfirmUpdateDevice(Device device, Version version)
        {
            var deviceResult = await _context.Devices.FirstOrDefaultAsync(x => x.Id == device.Id);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}