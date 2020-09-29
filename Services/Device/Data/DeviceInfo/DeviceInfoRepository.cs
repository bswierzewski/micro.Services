using Database;
using Database.Entities;
using Device.Dtos;
using Device.Params;
using Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Data.DeviceInfo
{
    public class DeviceInfoRepository : AppRepository, IDeviceInfoRepository
    {
        public DeviceInfoRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Kind>> GetKinds()
        {
            return await _context.Kinds.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.Include(x => x.Components).ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.Include(x => x.Components).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Component>> GetComponents(ComponentParams componentParams)
        {
            if (componentParams == null)
                componentParams = new ComponentParams();

            var query = _context.Components.Include(x => x.Category).AsQueryable();

            if (componentParams.CategoryId.HasValueGreaterThan(0))
                query = query.Where(x => x.CategoryId == componentParams.CategoryId);

            return await query.ToListAsync();
        }

        public async Task<ICollection<Component>> GetComponentsByIds(IEnumerable<int> deviceComponentIds)
        {
            return await _context.Components.Where(x => deviceComponentIds.Contains(x.Id)).ToListAsync();
        }

        public async Task<bool> UpdateComponents(int id, CategoryDto categoryDto)
        {
            var deviceComponents = await _context.Components.Where(x => categoryDto.ComponentIds.Contains(x.Id) || x.CategoryId == id).ToListAsync();

            if (deviceComponents.IsAny())
            {
                deviceComponents.ForEach(component =>
                {
                    if (categoryDto.ComponentIds.Any(componentId => componentId == component.Id))
                        component.CategoryId = id;
                    else
                        component.CategoryId = null;
                });

                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
