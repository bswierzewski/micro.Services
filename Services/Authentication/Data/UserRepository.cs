using Authentication.Dtos;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangeActivateUser(User user)
        {
            user.IsActive = !user.IsActive;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<User>> GetUsers()
        {
            return await _context.Users.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
