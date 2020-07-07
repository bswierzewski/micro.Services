using System.Threading.Tasks;

namespace Device.Data
{
    public interface IAppRepository
    {
        Task<bool> Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAllChanges();
    }
}