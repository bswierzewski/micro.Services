using Database.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Update.Dtos;

namespace Update.Data
{
    public interface IVersionRepository
    {
        IQueryable<GetVersionDto> GetVersions(Expression<Func<Database.Entities.Version, bool>> expression = null);
        Task<bool> UploadFile(FileData fileDatas);
        Task<bool> AddVersion(Database.Entities.Version newVersion);
    }
}
