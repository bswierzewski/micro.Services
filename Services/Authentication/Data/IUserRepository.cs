using Authentication.Dtos;
using Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authentication.Data
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task<IList<User>> GetUsers();
        Task<bool> ActivateUser(User user);
    }
}
