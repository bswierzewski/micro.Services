using Database;
using Database.Entities;
using Database.Enums;
using Device.Dtos;
using Device.Helpers;
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
        public async Task<DeviceDto> GetDevice(int id, DeviceRoleEnum roleEnum) => await GetDevice(id, n => n.Role == roleEnum);

        public async Task<IEnumerable<DeviceDto>> GetDevices() => await GetDevices(n => true);
        public async Task<IEnumerable<DeviceDto>> GetDevices(DeviceRoleEnum roleEnum) => await GetDevices(n => n.Role == roleEnum);

        private async Task<IEnumerable<DeviceDto>> GetDevices(Expression<Func<DeviceKind, bool>> @where)
        {
            var devices = await (from device in _context.Devices
                                 join types in _context.DeviceTypes.Where(@where) on device.DeviceTypeId equals types.Id
                                 select new DeviceDto
                                 {
                                     Id = device.Id,
                                     Created = device.Created,
                                     IsActive = device.LastActivated.CalculateActive(),
                                     LastActivated = device.LastActivated,
                                     IsArchival = device.IsArchival,
                                     Name = device.Name,
                                     MacAddress = device.MacAddress,
                                     PhotoUrl = device.PhotoUrl,
                                     Role = types.Role.ToString(),
                                     Kind = types.Kind,
                                 }).ToListAsync();

            return devices;
        }

        private async Task<DeviceDto> GetDevice(int id, Expression<Func<DeviceKind, bool>> @where)
        {
            var device = await (from devices in _context.Devices
                                join types in _context.DeviceTypes.Where(@where) on devices.DeviceTypeId equals types.Id
                                select new DeviceDto
                                {
                                    Id = devices.Id,
                                    Created = devices.Created,
                                    IsActive = devices.LastActivated.CalculateActive(),
                                    LastActivated = devices.LastActivated,
                                    IsArchival = devices.IsArchival,
                                    Name = devices.Name,
                                    MacAddress = devices.MacAddress,
                                    PhotoUrl = devices.PhotoUrl,
                                    Role = types.Role.ToString(),
                                    Kind = types.Kind,
                                }).FirstOrDefaultAsync(x => x.Id == id);

            return device;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}