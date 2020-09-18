using Database;
using Database.Entities.DeviceInfo;
using Device.Dtos;
using Device.Params;
using Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Device.Data.DeviceInfo
{
    public class DeviceInfoRepository : AppRepository, IDeviceInfoRepository
    {
        private readonly DataContext _context;
        public DeviceInfoRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories(int? categoryId = null)
        {
            Expression<Func<Database.Entities.DeviceInfo.Category, bool>> @where = n => true;

            if (categoryId != null)
                @where = n => n.Id == categoryId;

            return await _context.Categories
                .Include(x => x.DeviceComponents)
                .Where(@where)
                .ToListAsync();
        }

        public async Task<DeviceComponent> GetDeviceComponent(int componentId)
        {
            return await _context.DeviceComponents.FirstOrDefaultAsync(x => x.Id == componentId);
        }

        public async Task<IEnumerable<DeviceComponent>> GetDeviceComponents(DeviceComponentParams deviceComponentParams)
        {
            var query = _context.DeviceComponents.AsQueryable();

            if (deviceComponentParams.CategoryId.HasValueGreaterThan(0))
                query = query.Where(x => x.CategoryId == deviceComponentParams.CategoryId);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Kind>> GetKinds(int? kindId = null)
        {
            Expression<Func<Database.Entities.DeviceInfo.Kind, bool>> @where = n => true;

            if (kindId != null)
                @where = n => n.Id == kindId;

            return await _context.Kinds
                .Where(@where)
                .ToListAsync();
        }
    }
}
