using Database;
using System.Threading.Tasks;

namespace Update.Data
{
    public class AppRepository : IAppRepository
    {
        protected readonly DataContext _context;

        public AppRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> Find<T>(int primaryKey) where T : class
        {
            return await _context.FindAsync<T>(primaryKey);
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
