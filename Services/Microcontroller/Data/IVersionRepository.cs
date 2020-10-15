using Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Update.Data
{
    public interface IVersionRepository : IAppRepository
    {
        Task<Database.Entities.Version> GetVersion(int id);
        Task<IEnumerable<Database.Entities.Version>> GetVersions();
        Task<bool> UploadFile(FileData fileDatas);
        Task<bool> AddVersion(Database.Entities.Version newVersion);
    }
}
