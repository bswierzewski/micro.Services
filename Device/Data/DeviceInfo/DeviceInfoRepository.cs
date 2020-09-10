using Database;
using Device.Data.DeviceInfo.Category;
using Device.Data.DeviceInfo.DeviceComponent;
using Device.Dtos.DeviceInfo.Kind;
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

        public async Task<IEnumerable<GetCategoryDto>> GetCategories(int? categoryId = null)
        {
            Expression<Func<Database.Entities.DeviceInfo.Category, bool>> @where = n => true;

            if (categoryId != null)
                @where = n => n.Id == categoryId;

            return await _context.Categories
                .Include(x => x.DeviceComponents)
                .Where(@where)
                .Select(x =>
                    new GetCategoryDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Created = x.Created,
                        DeviceComponents = x.DeviceComponents.Select(component => new GetDeviceComponentDto
                        {
                            CategoryId = x.Id,
                            CategoryName = x.Name,
                            Id = component.Id,
                            Name = component.Name,
                            Created = component.Created

                        }).ToArray()
                    })
                .ToListAsync();
        }

        public async Task<IEnumerable<GetDeviceComponentDto>> GetDeviceComponents(int? componentId = null)
        {
            Expression<Func<Database.Entities.DeviceInfo.DeviceComponent, bool>> @where = n => true;

            if (componentId != null)
                @where = n => n.Id == componentId;

            return await _context.DeviceComponents
                .Where(@where)
                .Include(x => x.Category)
                .Select(x =>
                    new GetDeviceComponentDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Created = x.Created,
                        CategoryId = x.CategoryId,
                        CategoryName = x.Category.Name
                    })
                .ToListAsync();
        }

        public async Task<IEnumerable<GetKindDto>> GetKinds(int? kindId = null)
        {
            Expression<Func<Database.Entities.DeviceInfo.Kind, bool>> @where = n => true;

            if (kindId != null)
                @where = n => n.Id == kindId;

            return await _context.Kinds
                .Where(@where)
                .Select(x =>
                    new GetKindDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Created = x.Created
                    })
                .ToListAsync();
        }
    }
}
