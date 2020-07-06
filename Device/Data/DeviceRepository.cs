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

        public async Task<IEnumerable<GetDeviceDto>> GetDevices(short? deviceTypeId = null, short? deviceKindId = null)
        {
            Expression<Func<DeviceType, bool>> @whereType = n => true;
            Expression<Func<DeviceKind, bool>> @whereKind = n => true;

            if (deviceTypeId != null)
                @whereType = n => n.Id == deviceTypeId;

            if (deviceKindId != null)
                @whereKind = n => n.Id == deviceKindId;

            var devices = await (from device in _context.Devices
                                 join types in _context.DeviceTypes.Where(whereType) on device.DeviceTypeId equals types.Id into typesTemp
                                 from typesDef in typesTemp.DefaultIfEmpty()
                                 join kinds in _context.DeviceKinds.Where(whereKind) on device.DeviceKindId equals kinds.Id into kindsTemp
                                 from kindsDef in kindsTemp.DefaultIfEmpty()
                                 select new GetDeviceDto
                                 {
                                     Id = device.Id,
                                     Created = device.Created,
                                     Name = device.Name,
                                     MacAddress = device.MacAddress,
                                     PhotoUrl = device.PhotoUrl,
                                     Kind = kindsDef.Kind,
                                     Type = typesDef.Type,
                                 }).ToListAsync();

            return devices;
        }

        public async Task<GetDeviceDto> GetDevice(int id)
        {
            var device = await (from devices in _context.Devices
                                join types in _context.DeviceTypes on devices.DeviceTypeId equals types.Id
                                join kinds in _context.DeviceKinds on devices.DeviceKindId equals kinds.Id
                                select new GetDeviceDto
                                {
                                    Id = devices.Id,
                                    Created = devices.Created,
                                    Name = devices.Name,
                                    MacAddress = devices.MacAddress,
                                    PhotoUrl = devices.PhotoUrl,
                                    Kind = kinds.Kind,
                                    Type = types.Type,
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

        public async Task<DeviceKind> GetDeviceKind(string kind)
        {
            return await _context.DeviceKinds.FirstOrDefaultAsync(x => x.Kind == kind);
        }

        public async Task<IEnumerable<DeviceKind>> GetDeviceKinds()
        {
            return await _context.DeviceKinds.ToListAsync();
        }

        public async Task<bool> ExistsDeviceType(short deviceTypeId)
        {
            return await _context.DeviceTypes.AnyAsync(x => x.Id == deviceTypeId);
        }

        public async Task<bool> ExistsDeviceKind(short deviceKindId)
        {
            return await _context.DeviceKinds.AnyAsync(x => x.Id == deviceKindId);
        }

        public async Task<Database.Entities.Device> GetDeviceByMacAddress(string macAddress)
        {
            return await _context.Devices.FirstOrDefaultAsync(x => x.MacAddress == macAddress);
        }
    }
}