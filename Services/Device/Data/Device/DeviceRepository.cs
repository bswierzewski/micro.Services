using Database;
using Device.Params;
using Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Data
{
    public class DeviceRepository : AppRepository, IDeviceRepository
    {
        public DeviceRepository(DataContext context) : base(context) { }

        public async Task<int?> AddAddress(string macAddress)
        {
            var address = new Database.Entities.Address()
            {
                Created = DateTime.Now,
                Label = macAddress,
                IsConfirmed = true,
            };

            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();

            return address.Id;
        }

        public async Task<int?> GetAddressId(string address)
        {
            return await _context.Addresses.Where(x => x.Label == address).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<Database.Entities.Device> GetDevice(int id)
        {
            return await _context.Devices
                .Include(x => x.Address)
                .Include(x => x.Category)
                .Include(x => x.Kind)
                .Include(x => x.Component)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Database.Entities.Device>> GetDevices(DeviceParams deviceParams = null)
        {
            var deviceQuery = _context.Devices.Include(x => x.Component).Include(x => x.Kind).AsQueryable();

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