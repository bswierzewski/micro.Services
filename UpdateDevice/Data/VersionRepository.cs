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
                          select new VersionInfoDto
                          {
                              FileCreated = file.Created,
                              FileDataId = file.Id,
                              FileName = file.FileName,
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
