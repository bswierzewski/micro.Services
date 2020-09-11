using Database;
using Device.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<bool> ExistsDevice(string macAddress)
        {
            return await _context.Devices.AnyAsync(x => x.MacAddress == macAddress);
        }

        public IQueryable<DeviceDto> GetDevices(Expression<Func<Database.Entities.Device, bool>> expressionDevice = null)
        {
            Expression<Func<Database.Entities.Device, bool>> @whereDevice = n => true;

            if (expressionDevice != null)
                @whereDevice = expressionDevice;

            var devices = (from device in _context.Devices
                           .Include(device => device.Kind)
                           .Include(device => device.DeviceComponent)
                                .ThenInclude(component => component.Category)
                           .Where(whereDevice)
                           select new DeviceDto
                           {
                               Id = device.Id,
                               Created = device.Created,
                               Modified = device.Modified,
                               Name = device.Name,
                               MacAddress = device.MacAddress,
                               PhotoUrl = device.PhotoUrl,
                               Kind = device.Kind.Name,
                               Category = device.DeviceComponent.Category.Name,
                               DeviceComponent = device.DeviceComponent.Name,
                           });

            return devices;
        }

        public async Task<Database.Entities.Device> GetDevice(int deviceId)
        {
            return await _context.Devices.FirstOrDefaultAsync(x => x.Id == deviceId);
        }
    }
}