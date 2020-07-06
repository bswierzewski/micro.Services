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

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<GetDeviceDto>> GetDevices(int? deviceTypeId = null)
        {
            Expression<Func<DeviceType, bool>> @where = n => true;

            if (deviceTypeId != null)
                @where = n => n.Id == deviceTypeId;

            var devices = await (from device in _context.Devices
                                 join types in _context.DeviceTypes.Where(@where) on device.DeviceTypeId equals types.Id
                                 select new GetDeviceDto
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

        public async Task<GetDeviceDto> GetDevice(int id)
        {
            var device = await (from devices in _context.Devices
                                join types in _context.DeviceTypes on devices.DeviceTypeId equals types.Id
                                select new GetDeviceDto
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

        public async Task<DeviceType> GetDeviceType(string type)
        {
            return await _context.DeviceTypes.FirstOrDefaultAsync(x => x.Type == type);
        }

        public async Task<IEnumerable<DeviceType>> GetDeviceTypes()
        {
            return await _context.DeviceTypes.ToListAsync();
        }

        public async Task<bool> ExistsDevice(string macAddress)
        {
            return await _context.Devices.AnyAsync(x => x.MacAddress == macAddress);
        }

        public async Task<bool> ExistsDeviceType(short deviceTypeId)
        {
            return await _context.DeviceTypes.AnyAsync(x => x.Id == deviceTypeId);
        }

        public async Task<DeviceType> GetDeviceTypeById(short typeId)
        {
            return await _context.DeviceTypes.FirstOrDefaultAsync(x => x.Id == typeId);
        }
    }
}