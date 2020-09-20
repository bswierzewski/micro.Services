using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Device.Data
{
    public interface IAppRepository
    {
        Task<bool> Add<T>(T entity) where T : class;
        Task<bool> Delete<T>(T entity) where T : class;
        Task<T> Find<T>(int primaryKey) where T : class;
        Task<bool> SaveAllChangesAsync();
    }
}