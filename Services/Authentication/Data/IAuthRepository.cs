using Database.Entities;
using System.Threading.Tasks;

namespace Authentication.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string login, string password);
        Task<bool> UserExists(string username);
        Task<bool> EmailExists(string email);
    }
}