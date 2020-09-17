using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Update.Dtos;

namespace Update.Data
{
    public interface IVersionRepository
    {
        Task<Database.Entities.Version> GetVersion(int id);
        Task<IEnumerable<Database.Entities.Version>> GetVersions();
        Task<bool> UploadFile(FileData fileDatas);
        Task<bool> AddVersion(Database.Entities.Version newVersion);
    }
}
