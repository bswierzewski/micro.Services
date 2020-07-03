using System.Threading.Tasks;
using UpdateDevice.Dtos;

namespace UpdateDevice.Data
{
    public interface IVersionRepository
    {
        Task<VersionInfoDto[]> GetAllVersion();
    }
}
