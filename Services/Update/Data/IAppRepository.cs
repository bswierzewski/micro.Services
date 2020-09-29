using System.Threading.Tasks;

namespace Update.Data
{
    public interface IAppRepository
    {
        Task<T> Find<T>(int primaryKey) where T : class;
        Task<bool> Add<T>(T entity) where T : class;
        Task<bool> SaveAllChangesAsync();
    }
}
