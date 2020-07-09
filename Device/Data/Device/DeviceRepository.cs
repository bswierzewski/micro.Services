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
    public class DeviceRepository : AppRepository, IDeviceRepository
    {
        private readonly DataContext _context;
        public DeviceRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsDevice(string macAddress)
        {
            return await _context.Devices.AnyAsync(x => x.MacAddress == macAddress);
        }

        public async Task<GetDeviceDto> GetDeviceDtoById(int id)
            => await GetDeviceQueryable(expressionDevice: n => n.Id == id).FirstOrDefaultAsync();
        public async Task<IEnumerable<GetDeviceDto>> GetDevicesDto()
            => await GetDeviceQueryable().ToListAsync();
        public async Task<IEnumerable<GetDeviceDto>> GetDevicesDtoByType(short deviceTypeId)
            => await GetDeviceQueryable(expressionDevice: n => n.DeviceTypeId == deviceTypeId).ToListAsync();
        public async Task<IEnumerable<GetDeviceDto>> GetDevicesDtoByKind(short deviceKindId)
            => await GetDeviceQueryable(expressionDevice: n => n.DeviceKindId == deviceKindId).ToListAsync();

        private IQueryable<GetDeviceDto> GetDeviceQueryable(
            Expression<Func<Database.Entities.Device, bool>> expressionDevice = null)
        {
            Expression<Func<Database.Entities.Device, bool>> @whereDevice = n => true;

            if (expressionDevice != null)
                @whereDevice = expressionDevice;

            var devices = (from device in _context.Devices.Where(whereDevice)
                           join types in _context.DeviceTypes on device.DeviceTypeId equals types.Id into typesTemp
                           from typesDef in typesTemp.DefaultIfEmpty()
                           join kinds in _context.DeviceKinds on device.DeviceKindId equals kinds.Id into kindsTemp
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
                               VersionId = device.VersionId,
                           });

            return devices;
        }

        public async Task<Database.Entities.Device> GetDevice(int deviceId)
        {
            return await _context.Devices.FirstOrDefaultAsync(x => x.Id == deviceId);
        }

        public async Task<bool> IsDeviceType(short payload)
        {
            return await _context.DeviceTypes.AnyAsync(x => x.Id == payload);
        }

        public async Task<bool> IsDeviceKind(short payload)
        {
            return await _context.DeviceKinds.AnyAsync(x => x.Id == payload);
        }
    }
}