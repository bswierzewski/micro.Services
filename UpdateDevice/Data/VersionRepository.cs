using Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpdateDevice.Dtos;

namespace UpdateDevice.Data
{
    public class VersionRepository : IVersionRepository
    {
        private readonly DataContext _context;
        public VersionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<VersionInfoDto[]> GetAllVersion()
        {
            return await (from version in _context.Versions
                          join file in _context.FileDatas on version.FileDataId equals file.Id
                          join deviceType in _context.DeviceTypes on version.DeviceTypeId equals deviceType.Id
                          select new VersionInfoDto
                          {
                              FileCreated = file.Created,
                              DeviceType = deviceType.Name,
                              FileDataId = file.Id,
                              FileName = file.Name,
                              Major = version.Major,
                              Minor = version.Minor,
                              Patch = version.Patch,
                              VersionCreated = version.Created,
                              VersionId = version.Id
                          })
                .ToArrayAsync();
        }
    }
}
