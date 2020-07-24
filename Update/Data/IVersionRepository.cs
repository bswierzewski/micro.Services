using Database.Entities;
using System.Threading.Tasks;
using Update.Dtos;

namespace Update.Data
{
    public interface IVersionRepository
    {
        Task<bool> UploadFile(FileData fileDatas);
        Task<bool> AddVersion(Version newVersion);
    }
}
