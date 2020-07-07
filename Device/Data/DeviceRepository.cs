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

        #region Device

        public async Task<GetDeviceDto> GetDeviceDtoById(int id) => await GetDeviceQueryable(expressionDevice: n => n.Id == id).FirstOrDefaultAsync();
        public async Task<IEnumerable<GetDeviceDto>> GetDevicesDto() => await GetDeviceQueryable().ToListAsync();
        public async Task<IEnumerable<GetDeviceDto>> GetDevicesDtoByType(short deviceTypeId) => await GetDeviceQueryable(expressionType: n => n.Id == deviceTypeId).ToListAsync();
        public async Task<IEnumerable<GetDeviceDto>> GetDevicesDtoByKind(short deviceKindId) => await GetDeviceQueryable(expressionKind: n => n.Id == deviceKindId).ToListAsync();

        private IQueryable<GetDeviceDto> GetDeviceQueryable(Expression<Func<Database.Entities.Device, bool>> expressionDevice = null,
            Expression<Func<DeviceType, bool>> expressionType = null,
            Expression<Func<DeviceKind, bool>> expressionKind = null)
        {
            Expression<Func<Database.Entities.Device, bool>> @whereDevice = n => true;
            Expression<Func<DeviceType, bool>> @whereType = n => true;
            Expression<Func<DeviceKind, bool>> @whereKind = n => true;

            if (expressionDevice != null)
                @whereDevice = expressionDevice;

            if (expressionType != null)
                @whereType = expressionType;

            if (expressionKind != null)
                @whereKind = expressionKind;

            var devices = (from device in _context.Devices.Where(whereDevice)
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
                           }).AsQueryable();

            return devices;
        }

        #endregion

        #region DeviceType

        public async Task<bool> IsDeviceType(short deviceTypeId)
        {
            return await _context.DeviceTypes.AnyAsync(x => x.Id == deviceTypeId);
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

        #endregion

        #region DeviceKind

        public async Task<bool> IsDeviceKind(short deviceKindId)
        {
            return await _context.DeviceKinds.AnyAsync(x => x.Id == deviceKindId);
        }

        public async Task<DeviceKind> GetDeviceKind(string kind)
        {
            return await _context.DeviceKinds.FirstOrDefaultAsync(x => x.Kind == kind);
        }

        public async Task<DeviceKind> GetDeviceKind(int kindId)
        {
            return await _context.DeviceKinds.FirstOrDefaultAsync(x => x.Id == kindId);
        }

        public async Task<IEnumerable<DeviceKind>> GetDeviceKinds()
        {
            return await _context.DeviceKinds.ToListAsync();
        }

        #endregion
    }
}