using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Device.Data
{
    public class DeviceKindRepository : AppRepository, IDeviceKindRepository
    {
        private readonly DataContext _context;
        public DeviceKindRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsDeviceKind(string kind)
        {
            return await _context.DeviceKinds.AnyAsync(x => x.Kind == kind);
        }

        public async Task<Kind> GetDeviceKind(string kind)
        {
            return await _context.DeviceKinds.FirstOrDefaultAsync(x => x.Kind == kind);
        }

        public async Task<Kind> GetDeviceKind(int kindId)
        {
            return await _context.DeviceKinds.FirstOrDefaultAsync(x => x.Id == kindId);
        }

        public async Task<IEnumerable<Kind>> GetDeviceKinds()
        {
            return await _context.DeviceKinds.ToListAsync();
        }

    }
}