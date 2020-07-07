using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public class DeviceTypeRepository : AppRepository, IDeviceTypeRepository
    {
        private readonly DataContext _context;
        public DeviceTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsDeviceType(string type)
        {
            return await _context.DeviceTypes.AnyAsync(x => x.Type == type);
        }

        public async Task<DeviceType> GetDeviceType(string type)
        {
            return await _context.DeviceTypes.FirstOrDefaultAsync(x => x.Type == type);
        }

        public async Task<DeviceType> GetDeviceType(int typeId)
        {
            return await _context.DeviceTypes.FirstOrDefaultAsync(x => x.Id == typeId);
        }

        public async Task<IEnumerable<DeviceType>> GetDeviceTypes()
        {
            return await _context.DeviceTypes.ToListAsync();
        }
    }
}