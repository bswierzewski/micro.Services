using Database;
using Device.Dtos;
using Device.Params;
using Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Device.Data
{
    public class DeviceRepository : AppRepository, IDeviceRepository
    {
        private readonly DataContext _context;
        public DeviceRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsDevice(int addressId)
        {
            return await _context.Devices.AnyAsync(x => x.AddressId == addressId);
        }

        public async Task<Database.Entities.Device> GetDevice(int deviceId)
        {
            return await _context.Devices.FirstOrDefaultAsync(x => x.Id == deviceId);
        }

        public async Task<ICollection<Database.Entities.Device>> GetDevices(DeviceParams deviceParams = null)
        {
            var deviceQuery = _context.Devices.AsQueryable();

            if (deviceParams is null)
                return await deviceQuery.ToListAsync();

            if (deviceParams.KindId.HasValueGreaterThan(0))
                deviceQuery = deviceQuery.Where(x => x.KindId == deviceParams.KindId);

            if (deviceParams.CategoryId.HasValueGreaterThan(0))
                deviceQuery = deviceQuery.Where(x => x.CategoryId == deviceParams.CategoryId);


            return await deviceQuery.ToListAsync();
        }
    }
}