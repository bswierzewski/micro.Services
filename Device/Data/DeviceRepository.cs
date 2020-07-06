using Database;
using Database.Entities;
using Device.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Device.Data
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DataContext _context;

        public DeviceRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<DeviceDto> GetDevice(int id) => await GetDevice(id, n => true);

        public async Task<IEnumerable<DeviceDto>> GetDevices() => await GetDevices(n => true);

        private async Task<IEnumerable<DeviceDto>> GetDevices(Expression<Func<DeviceType, bool>> @where)
        {
            var devices = await (from device in _context.Devices
                                 join types in _context.DeviceTypes.Where(@where) on device.DeviceTypeId equals types.Id
                                 select new DeviceDto
                                 {
                                     Id = device.Id,
                                     Created = device.Created,
                                     Name = device.Name,
                                     MacAddress = device.MacAddress,
                                     PhotoUrl = device.PhotoUrl,
                                     Kind = types.Type,
                                 }).ToListAsync();

            return devices;
        }

        private async Task<DeviceDto> GetDevice(int id, Expression<Func<DeviceType, bool>> @where)
        {
            var device = await (from devices in _context.Devices
                                join types in _context.DeviceTypes.Where(@where) on devices.DeviceTypeId equals types.Id
                                select new DeviceDto
                                {
                                    Id = devices.Id,
                                    Created = devices.Created,
                                    Name = devices.Name,
                                    MacAddress = devices.MacAddress,
                                    PhotoUrl = devices.PhotoUrl,
                                    Kind = types.Type,
                                }).FirstOrDefaultAsync(x => x.Id == id);

            return device;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}